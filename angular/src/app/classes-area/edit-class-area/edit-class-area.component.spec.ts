import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditClassAreaComponent } from './edit-class-area.component';

describe('EditClassAreaComponent', () => {
  let component: EditClassAreaComponent;
  let fixture: ComponentFixture<EditClassAreaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditClassAreaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditClassAreaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
