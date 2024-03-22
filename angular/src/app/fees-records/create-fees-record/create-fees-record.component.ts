import { PrinterService } from './../../../shared/helpers/printer.service';
import { DecimalPipe } from '@angular/common';
import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { CreateClassAreaDto, StudentDto, TeacherDto, ClassAreaServiceProxy, UserServiceProxy, StudentServiceProxy, TeacherServiceProxy, CreateFeesRecordDto, FeesRecordServiceProxy } from '@shared/service-proxies/service-proxies';
import { UserBaseComponent } from '@shared/user-base/user-base.component';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-fees-record',
  templateUrl: './create-fees-record.component.html',
  styleUrls: ['./create-fees-record.component.css']
})
export class CreateFeesRecordComponent extends UserBaseComponent implements OnInit {
  saving = false;
  user:CreateFeesRecordDto = new CreateFeesRecordDto();
  @Output() onSave = new EventEmitter<any>();
  ClassArea:number;
  notify: any;
  students:StudentDto[] = [];
  teachers:TeacherDto[] = [];
  studentDetail: StudentDto = new StudentDto();

  constructor(
    injector: Injector,
    private _classAreaService: ClassAreaServiceProxy,
    public _userService: UserServiceProxy,
    public bsModalRef: BsModalRef,
    private _studentService: StudentServiceProxy,
    private _teacherService: TeacherServiceProxy,
    private _feesRecordService: FeesRecordServiceProxy,
    private printer: PrinterService
      ) {
    super(injector,_userService,bsModalRef);
  }
  ngOnInit(): void {
    //this.user.isActive = true;
    this.getStudents();
    //this.getTeachers();
  }
  save(): void {
    this.saving = true;
    this.user.total = this.studentDetail.totalFees;
    //this.user.ClassArea= this.ClassArea;
    console.log('save',this.user);
    //this.user.startTime = new Date(this._datePipe.transform( DateHelper.toLocalDate(this.dueDate), 'yyyy-MM-dd'));//DateHelper.convertDateTimeToString(this.dueDate, AppConsts.dateFormate);
    this._feesRecordService
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

  getStudentById(){
    let result = this._studentService.getById(this.user.studentId).subscribe((result) => {
      this.studentDetail = result;
      console.log('this.studentDetail',this.studentDetail);
    });
  }

  getTeachers(){
    let result = this._teacherService.getAll().subscribe((result) => {
      this.teachers = result.items;
    });
  }

  calculateValues(event){
    //this.user.remaining  = this.studentDetail.totalFees - event - this.user.discount;
  }

}

