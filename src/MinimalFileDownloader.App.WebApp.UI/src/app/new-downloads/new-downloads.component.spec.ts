import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewDownloadsComponent } from './new-downloads.component';

describe('NewDownloadsComponent', () => {
  let component: NewDownloadsComponent;
  let fixture: ComponentFixture<NewDownloadsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewDownloadsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewDownloadsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
