import { Router } from '@angular/router';
import { CreateFeesRecordComponent } from './create-fees-record/create-fees-record.component';
import { Component, Injector, Input, OnInit } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { FeesRecordDto, FeesRecordServiceProxy, StudentDto, StudentServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
class PagedFeesRecordResultRequestDto extends PagedRequestDto{
  keyword: string;
  studentId: number;
}
@Component({
  selector: 'app-fees-records',
  templateUrl: './fees-records.component.html',
  styleUrls: ['./fees-records.component.css']
})
export class FeesRecordsComponent extends PagedListingComponentBase<StudentDto> {
  feesRecords: FeesRecordDto[] = [];
  keyword = '';
  @Input() centerId:number;
  @Input() studentId:number;
  constructor(
    injector: Injector,
    private _router:Router,
    private _stuentService: StudentServiceProxy,
    private _modalService: BsModalService,
    private _feesRecordService: FeesRecordServiceProxy
  ) {
    super(injector);
  }

  list(
    request: PagedFeesRecordResultRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;
    request.studentId = this.studentId;

    this._feesRecordService
      .getPagedResult(request.keyword,request.studentId, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result) => {
        this.feesRecords = result.items;
        this.showPaging(result, pageNumber);
      });
  }


  delete(model: StudentDto): void {
    abp.message.confirm(
      this.l('UserDeleteWarningMessage', model.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._stuentService
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


  showCreateOrEditRoleDialog(id?: number): void {
    let createOrEditRoleDialog: BsModalRef;
    if (!id) {
      createOrEditRoleDialog = this._modalService.show(
        CreateFeesRecordComponent,
        {
          class: 'modal-lg',
          initialState:{
            centerId:this.centerId,
          }
        }
      );
    } else {
      createOrEditRoleDialog = this._modalService.show(
        CreateFeesRecordComponent,
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

  goToDetails(fees: FeesRecordDto) {
    this._router.navigate(['app/fees-detail'], {
      queryParams: { feesRecordId: fees.id },
    })
  }
}

