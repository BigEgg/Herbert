import { HerbertUIPage } from './app.po';

describe('herbert-ui App', function() {
  let page: HerbertUIPage;

  beforeEach(() => {
    page = new HerbertUIPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
