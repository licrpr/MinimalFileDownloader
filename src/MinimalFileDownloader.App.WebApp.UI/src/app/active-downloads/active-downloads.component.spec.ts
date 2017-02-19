import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ActiveDownloadsComponent } from './active-downloads.component';

describe('ActiveDownloadsComponent', () => {
  let component: ActiveDownloadsComponent;
  let fixture: ComponentFixture<ActiveDownloadsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ActiveDownloadsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ActiveDownloadsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
