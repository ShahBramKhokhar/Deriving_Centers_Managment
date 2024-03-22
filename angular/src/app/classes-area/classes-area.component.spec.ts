import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassesAreaComponent } from './classes-area.component';

describe('ClassesAreaComponent', () => {
  let component: ClassesAreaComponent;
  let fixture: ComponentFixture<ClassesAreaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClassesAreaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClassesAreaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
