import { ClassAreaDto } from './../../../shared/service-proxies/service-proxies';
import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { DateHelper } from '@shared/helpers/DateHelper';
import { CreateClassAreaDto, StudentDto, TeacherDto, ClassAreaServiceProxy, UserServiceProxy, StudentServiceProxy, TeacherServiceProxy } from '@shared/service-proxies/service-proxies';
import { UserBaseComponent } from '@shared/user-base/user-base.component';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-edit-class-area',
  templateUrl: './edit-class-area.component.html',
  styleUrls: ['./edit-class-area.component.css']
})
export class EditClassAreaComponent extends UserBaseComponent implements OnInit {
  saving = false;
  createClassDto:ClassAreaDto = new ClassAreaDto();
  @Output() onSave = new EventEmitter<any>();
  ClassArea:number;
  notify: any;
  students:StudentDto[] = [];
  teachers:TeacherDto[] = [];
  id:number;
  start:string;
  end:string;

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
    //this.createClassDto.studentIds = [];
  }
  ngOnInit(): void {
    //this.user.isActive = true;
    this._classAreaService.getById(this.id).subscribe((result) => {
      this.createClassDto = result;
      console.log('result sss',result);
    });
    this.getStudents();
    this.getTeachers();
  }
  save(): void {
    this.saving = true;
    //this.user.ClassArea= this.ClassArea;
    console.log('save',this.createClassDto);
    let request = new CreateClassAreaDto();
    request.id = this.createClassDto.id;
    request.title = this.createClassDto.title;
    request.descrtipton = this.createClassDto.descrtipton;
    request.teacherId = this.createClassDto.teacherId;
    request.studentIds = this.createClassDto.studentIds;

    request.startTime =  this._datePipe.transform( DateHelper.toLocalDate(new Date(this.start)), 'yyyy-MM-dd HH:mm:ss');
    request.endTime = this._datePipe.transform( DateHelper.toLocalDate(new Date(this.end)), 'yyyy-MM-dd HH:mm:ss');
    this._classAreaService
      .update(request)
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

