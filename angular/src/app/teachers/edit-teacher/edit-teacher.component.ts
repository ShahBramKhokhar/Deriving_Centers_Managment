import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { TeacherDto, TeacherServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { UserBaseComponent } from '@shared/user-base/user-base.component';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-edit-teacher',
  templateUrl: './edit-teacher.component.html',
  styleUrls: ['./edit-teacher.component.css']
})
export class EditTeacherComponent extends UserBaseComponent implements OnInit {
  saving = false;
  user:TeacherDto = new TeacherDto();
  id:number;
  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    private _teacherService: TeacherServiceProxy,
    public _userService: UserServiceProxy,
      public bsModalRef: BsModalRef
  ) {
    super(injector,_userService,bsModalRef);
  }

  ngOnInit(): void {

    this._teacherService.getById(this.id).subscribe((result) => {
      debugger;
      this.user = result;

    });
  }

  save(): void {
    this.saving = true;
    this._teacherService
      .update(this.user)
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
}


