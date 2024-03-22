import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { CenterServiceProxy, CentreDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { UserBaseComponent } from '@shared/user-base/user-base.component';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-edit-center',
  templateUrl: './edit-center.component.html',
  styleUrls: ['./edit-center.component.css']
})
export class EditCenterComponent extends UserBaseComponent implements OnInit {
  saving = false;
  centerDto:CentreDto = new CentreDto();
  id:number;
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

    this._centerService.getById(this.id).subscribe((result) => {
      this.centerDto = result;

    });
  }

  save(): void {
    this.saving = true;
    this._centerService
      .update(this.centerDto)
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


