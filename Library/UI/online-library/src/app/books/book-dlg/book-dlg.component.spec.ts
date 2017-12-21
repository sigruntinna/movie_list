import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookDlgComponent } from './book-dlg.component';

describe('BookDlgComponent', () => {
  let component: BookDlgComponent;
  let fixture: ComponentFixture<BookDlgComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookDlgComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookDlgComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
