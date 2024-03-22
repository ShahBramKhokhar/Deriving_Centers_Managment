import { Router } from '@angular/router';
import { Component, Injector, Input, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { StudentDto, StudentServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { CreateStudentComponent } from './create-student/create-student.component';
import { EditStudentComponent } from './edit-student/edit-student.component';

class PagedStudentRequestDto extends PagedRequestDto{
  keyword: string;
}

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css'],
  animations: [appModuleAnimation()]
})
export class StudentsComponent extends PagedListingComponentBase<StudentDto> {
  students: StudentDto[] = [];
  keyword = '';
  @Input() centerId:number;
  constructor(
    injector: Injector,
    private _stuentService: StudentServiceProxy,
    private _modalService: BsModalService,
    private _router: Router
  ) {
    super(injector);
  }

  list(
    request: PagedStudentRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;

    this._stuentService
      .getPagedResult(request.keyword,this.centerId, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result) => {
        this.students = result.items;
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
        CreateStudentComponent,
        {
          class: 'modal-lg',
          initialState:{
            centerId:this.centerId,
          }
        }
      );
    } else {
      createOrEditRoleDialog = this._modalService.show(
        EditStudentComponent,
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

  goTStudentDetail(id:number) {
    this._router.navigate(['app/student-detail'], {
      queryParams: { studentId: id },
    })
  }
}
