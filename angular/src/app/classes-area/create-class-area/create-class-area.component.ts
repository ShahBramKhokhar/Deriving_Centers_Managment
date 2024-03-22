import { DateHelper } from './../../../shared/helpers/DateHelper';
import { StudentDto, TeacherDto, TeacherServiceProxy } from './../../../shared/service-proxies/service-proxies';
import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { ClassAreaServiceProxy, CreateClassAreaDto, StudentServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { UserBaseComponent } from '@shared/user-base/user-base.component';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-create-class-area',
  templateUrl: './create-class-area.component.html',
  styleUrls: ['./create-class-area.component.css']
})
export class CreateClassAreaComponent extends UserBaseComponent implements OnInit {
  saving = false;
  user:CreateClassAreaDto = new CreateClassAreaDto();
  @Output() onSave = new EventEmitter<any>();
  ClassArea:number;
  notify: any;
  students:StudentDto[] = [];
  teachers:TeacherDto[] = [];

  constructor(
    injector: Injector,
    private _classAreaService: ClassAreaServiceProxy,
    public _userService: UserServiceProxy,
    public bsModalRef: BsModalRef,
    private _studentService: StudentServiceProxy,
    private _teacherService: TeacherServiceProxy,
    private _datePipe: DatePipe
  ) {
    super(injector,_userService,bsModalRef);
    this.user.studentIds = [];
  }
  ngOnInit(): void {
    //this.user.isActive = true;
    this.getStudents();
    this.getTeachers();
  }
  save(): void {
    this.saving = true;
    //this.user.ClassArea= this.ClassArea;
    console.log('save',this.user);
    this.user.startTime = this._datePipe.transform( DateHelper.toLocalDate(new Date(this.user.startTime)), 'yyyy-MM-dd HH:mm:ss');
    this.user.endTime = this._datePipe.transform( DateHelper.toLocalDate(new Date(this.user.endTime)), 'yyyy-MM-dd HH:mm:ss');
    this._classAreaService
      .create(this.user)
      .subscribe(
        () => {
          this.notify.info(this.l('SavedSuccessfully'));
          this.bsModalRef.hide();
          this.onSave.emit();
        },
        () => {
          this.saving = false;
        }
      );
  }

  getStudents(){
    let result = this._studentService.getAll().subscribe((result) => {
      this.students = result.items;
    });
  }

  getTeachers(){
    let result = this._teacherService.getAll().subscribe((result) => {
      this.teachers = result.items;
    });
  }

}
