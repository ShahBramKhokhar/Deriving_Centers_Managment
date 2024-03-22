import { Base64Image } from '@shared/modals/base64image';
import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { CreateStudentDto, StudentServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { UserBaseComponent } from '@shared/user-base/user-base.component';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { constants } from 'buffer';
import { AppConsts } from '@shared/AppConsts';

@Component({
  selector: 'app-create-student',
  templateUrl: './create-student.component.html',
  styleUrls: ['./create-student.component.css']
})
export class CreateStudentComponent extends UserBaseComponent implements OnInit {
  saving = false;
  user:CreateStudentDto = new CreateStudentDto();
  @Output() onSave = new EventEmitter<any>();
  centerId:number;
  isPhotoUploaded = true;
  constructor(
    injector: Injector,
    private _studentService: StudentServiceProxy,
    public _userService: UserServiceProxy,
      public bsModalRef: BsModalRef
  ) {
    super(injector,_userService,bsModalRef);
  }

  ngOnInit(): void {

    this.user.isActive = true;

  }

  onFileUploadHandler(image: Base64Image) {
    this.user.imageBase64String = image.ImageBase64String;
  }

  save(): void {
    this.saving = true;
    this.user.centerId = this.centerId;
    this.user.password = '123qwe';
    this.user.userName = this.user.emailAddress;
    this._studentService
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


