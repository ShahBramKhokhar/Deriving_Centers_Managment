import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FeesRecordDto, FeesRecordServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PrinterService } from '@shared/helpers/printer.service';

@Component({
  selector: 'app-fees-detail',
  templateUrl: './fees-detail.component.html',
  styleUrls: ['./fees-detail.component.css']
})
export class FeesDetailComponent implements OnInit {
  feesRecordId:number;
  feesRecord: FeesRecordDto;
  constructor(private _route: ActivatedRoute,
    private _feesRecordService: FeesRecordServiceProxy,
    private printer: PrinterService,

    ) { }

  ngOnInit() {
    this._route.queryParams.subscribe(params => {

      if (params.feesRecordId) {
        this.feesRecordId = Number(params.feesRecordId);

        this._feesRecordService.getById(this.feesRecordId).subscribe(res=>{
            this.feesRecord = res;
        });
      }
    });
  }

  public print() {
    let style = `
    table#target-table{
      width:100%;
      border-collapse: collapse;
    }
    table#target-table td,th{
      border: 1px solid black;
    }
    `
    this.printer.print(document.getElementById("printableDiv").innerHTML, style, "Fees Detail", "landscape", 'A4', true);
  }

}
