import { Component, Injector, Input, OnInit } from '@angular/core';
import { CreateClassAreaComponent } from './create-class-area/create-class-area.component';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { ClassAreaDto, ClassAreaDtoPagedResultDto, ClassAreaServiceProxy } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { EditClassAreaComponent } from './edit-class-area/edit-class-area.component';

class PagedClassAreaResultRequestDto extends PagedRequestDto{
  keyword: string;
}
@Component({
  selector: 'app-classes-area',
  templateUrl: './classes-area.component.html',
  styleUrls: ['./classes-area.component.css'],
  animations: [appModuleAnimation()]
})
export class ClassesAreaComponent extends PagedListingComponentBase<ClassAreaDto> {
  classArea: ClassAreaDto [] = [];
  keyword = '';
  @Input()centerId:number;
  constructor(
    injector: Injector,
    private _classAreaService: ClassAreaServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  list(
    request: PagedClassAreaResultRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;

    this._classAreaService
      .getPagedResult(request.keyword, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result) => {
        console.log('Class Area',result);
        this.classArea = result.items;
        this.showPaging(result, pageNumber);
      });
  }


  delete(model: ClassAreaDto): void {
    abp.message.confirm(
      this.l('UserDeleteWarningMessage', 'UserDeleteWarningMessage'),
      undefined,
      (result: boolean) => {
        if (result) {
          this._classAreaService
            .delete(model.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l('SuccessfullyDeleted'));
                this.refresh();
              })
            )
            .subscribe(() => {});
        }
      }
    );
  }

  ngOnInit(): void {

    this.getDataPage(1);
  }

  showCreateOrEditClassDialog(id?: number): void {
    let createOrEditRoleDialog: BsModalRef;
    if (!id) {
      createOrEditRoleDialog = this._modalService.show(
        CreateClassAreaComponent,
        {
          class: 'modal-lg',
          initialState:{
            ClassArea:this.classArea,
          }
        }
      );
    } else {
      createOrEditRoleDialog = this._modalService.show(
        EditClassAreaComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditRoleDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}
