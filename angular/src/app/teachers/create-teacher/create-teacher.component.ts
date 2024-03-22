import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { CreateTeacherDto, TeacherDto, TeacherServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { UserBaseComponent } from '@shared/user-base/user-base.component';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-teacher',
  templateUrl: './create-teacher.component.html',
  styleUrls: ['./create-teacher.component.css']
})
export class CreateTeacherComponent extends UserBaseComponent implements OnInit {
  saving = false;
  user:CreateTeacherDto = new CreateTeacherDto();

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

    this.user.isActive = true;

  }

  save(): void {
    this.saving = true;
    this._teacherService
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
}


