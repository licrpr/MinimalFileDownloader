import { Component, OnInit } from '@angular/core';
import { ApiClientService } from '../services/index';
import { BrowseResponseItem, StartDownloadRequest, StartDownloadRequestItem } from '../services/models';


@Component({
  selector: 'app-new-downloads',
  templateUrl: './new-downloads.component.html',
  styleUrls: ['./new-downloads.component.css']
})
export class NewDownloadsComponent implements OnInit {

  title = 'Start a new download ...';

  public activeItems: string[] = [];
  public items: BrowseResponseItem[];

  constructor(private client: ApiClientService) { }

  ngOnInit() {
    this.client.BrowseDirectory()
      .subscribe(o => this.items = o);
  }

  canSyncFolder(item: BrowseResponseItem): boolean {
    if (item.type != 'folder') {
      return false;
    }

    const filesToDownload = this.items
      .filter(o => o.type == 'file' && o.path.startsWith(item.path))
      .map(o => {
        const startItem = new StartDownloadRequestItem();
        startItem.sourcePath = o.path;
        startItem.destinationPath = o.path;
        return startItem;
      });
    return filesToDownload.length > 0;
  }

  syncFolder(item: BrowseResponseItem): void {
    const filesToDownload = this.items
      .filter(o => o.type == 'file' && o.path.startsWith(item.path))
      .map(o => {
        const startItem = new StartDownloadRequestItem();
        startItem.sourcePath = o.path;
        startItem.destinationPath = o.path;
        return startItem;
      });

    var newActiveItems = this.items
      .filter(o => o.path.startsWith(item.path))
      .map(o => o.path);
    this.downloadFiles(filesToDownload, newActiveItems);
  }

  canDownloadFile(item: BrowseResponseItem): boolean {
    if (item.type != 'file') {
      return false;
    }

    return true;
  }

  downloadFile(item: BrowseResponseItem): void {
    const startItem = new StartDownloadRequestItem();
    startItem.sourcePath = item.path;
    startItem.destinationPath = item.path.split('/').pop();

    const filesToDownload = [startItem];
    this.downloadFiles(filesToDownload, [startItem.sourcePath]);
  }

  isWorkingOn(item: BrowseResponseItem) {
    return this.activeItems.indexOf(item.path) >= 0;
  }

  downloadFiles(filesToDownload: StartDownloadRequestItem[], newActiveItems: string[]) {
    newActiveItems
      .forEach(o => this.activeItems.push(o));

    var removeActive = (): void => {
      newActiveItems.forEach(o => this.activeItems.splice(this.activeItems.indexOf(o), 1));
    };

    const request = new StartDownloadRequest();
    request.paths = filesToDownload;
    this.client.StartDownload(request)
      .subscribe(() => {
        removeActive();
      }, e => {
        removeActive();
        alert(e);
      }, () => {
      });
  }
}
