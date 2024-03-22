import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import {  CenterServiceProxy, CreateCentreDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { UserBaseComponent } from '@shared/user-base/user-base.component';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-center',
  templateUrl: './create-center.component.html',
  styleUrls: ['./create-center.component.css']
})
export class CreateCenterComponent extends UserBaseComponent implements OnInit {
  saving = false;
  user:CreateCentreDto = new CreateCentreDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    private _centerService: CenterServiceProxy,
    public _userService: UserServiceProxy,
      public bsModalRef: BsModalRef
  ) {
    super(injector,_userService,bsModalRef);
  }

  ngOnInit(): void {

    //this.user.isActive = true;

  }

  save(): void {
    this.saving = true;
    this._centerService
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


