import { OCMTemplatePage } from './app.po';

describe('OCM App', function() {
  let page: OCMTemplatePage;

  beforeEach(() => {
    page = new OCMTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
