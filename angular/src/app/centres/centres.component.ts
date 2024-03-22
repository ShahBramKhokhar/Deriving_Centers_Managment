import { Component, Injector, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { CenterServiceProxy, CentreDto } from '@shared/service-proxies/service-proxies';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { CreateCenterComponent } from './create-center/create-center.component';
import { EditCenterComponent } from './edit-center/edit-center.component';

class PagedCenterRequestDto extends PagedRequestDto {
  keyword: string;
}

@Component({
  selector: 'app-centres',
  templateUrl: './centres.component.html',
  styleUrls: ['./centres.component.css'],
  animations: [appModuleAnimation()]
})
export class CentresComponent extends PagedListingComponentBase<CentreDto> {
  centers: CentreDto[] = [];
  keyword = '';
  constructor(
    injector: Injector,
    private _centerService: CenterServiceProxy,
    private _modalService: BsModalService,
    private router: Router,
  ) {
    super(injector);
  }

  list(
    request: PagedCenterRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;

    this._centerService
      .getPagedResult(request.keyword, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result) => {
        this.centers = result.items;
        this.showPaging(result, pageNumber);
      });
  }
  delete(center: CentreDto): void {
    abp.message.confirm(
      this.l('UserDeleteWarningMessage', center.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._centerService
            .delete(center.id)
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
        CreateCenterComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditRoleDialog = this._modalService.show(
        EditCenterComponent,
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

  goTCenterDetails(center: CentreDto) {
    this.router.navigate(['app/center-detail'], {
      queryParams: { centerId: center.id },
    })
  }
}
