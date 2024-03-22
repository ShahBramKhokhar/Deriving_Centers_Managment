import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateClassAreaComponent } from './create-class-area.component';

describe('CreateClassAreaComponent', () => {
  let component: CreateClassAreaComponent;
  let fixture: ComponentFixture<CreateClassAreaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateClassAreaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateClassAreaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
