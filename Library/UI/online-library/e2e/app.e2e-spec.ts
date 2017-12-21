import { OnlineLibraryPage } from './app.po';

describe('online-library App', function() {
  let page: OnlineLibraryPage;

  beforeEach(() => {
    page = new OnlineLibraryPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
