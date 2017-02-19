import { Injectable } from '@angular/core';
import { Http, Response, Headers, URLSearchParams } from '@angular/http';
import { BrowseResponseItem, DownloadResponse, StartDownloadRequest, StartDownloadRequestItem, StopDownloadRequest } from './models';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';

@Injectable()
export class ApiClientService {
	domain: string;

	constructor(public http: Http) {
		this.domain = '';
	}

	public BrowseDirectory(Directory?: string, Recursive?: boolean, Type?: string):Observable<BrowseResponseItem[]> {
		let payload = {};
		let queryParameters: URLSearchParams = new URLSearchParams();
		let headers = new Headers();
		headers.append('Content-Type', 'application/json');

		if (Directory !== undefined) {
			queryParameters['Directory'] = Directory;
		}


		if (Recursive !== undefined) {
			queryParameters['Recursive'] = Recursive;
		}


		if (Type !== undefined) {
			queryParameters['Type'] = Type;
		}

		let uri = `/api/v0/browser`;

		return this.http
			.get(this.domain + uri, { headers: headers, search: queryParameters })
			.map((res: Response) => {
				return res.json() as BrowseResponseItem[];
			});
	}

	public GetDownloads():Observable<DownloadResponse[]> {
		let payload = {};
		let headers = new Headers();
		headers.append('Content-Type', 'application/json');

		let uri = `/api/v0/downloads`;

		return this.http
			.get(this.domain + uri, { headers: headers })
			.map((res: Response) => {
				return res.json() as DownloadResponse[];
			});
	}

	public StartDownload(request: StartDownloadRequest):Observable<any> {
		let payload = {};
		let headers = new Headers();
		headers.append('Content-Type', 'application/json');


		payload['request'] = request;
		let uri = `/api/v0/downloads`;

		return this.http
			.post(this.domain + uri, JSON.stringify(request), { headers: headers })
			.map((res: Response) => {
				return res;
			});
	}

	public StopDownload(request: StopDownloadRequest):Observable<any> {
		let payload = {};
		let headers = new Headers();
		headers.append('Content-Type', 'application/json');


		payload['request'] = request;
		let uri = `/api/v0/downloads`;

		return this.http
			.delete(this.domain + uri, { headers: headers })
			.map((res: Response) => {
				return res;
			});
	}
}
