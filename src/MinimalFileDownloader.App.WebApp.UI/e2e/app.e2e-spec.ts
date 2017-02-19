import { MinimalFileDownloader.App.WebApp.UIPage } from './app.po';

describe('minimal-file-downloader.app.web-app.ui App', () => {
  let page: MinimalFileDownloader.App.WebApp.UIPage;

  beforeEach(() => {
    page = new MinimalFileDownloader.App.WebApp.UIPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
