import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserDlgComponent } from './user-dlg.component';

describe('UserDlgComponent', () => {
  let component: UserDlgComponent;
  let fixture: ComponentFixture<UserDlgComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserDlgComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserDlgComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
