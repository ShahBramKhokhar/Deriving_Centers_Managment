import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PrinterService {

  constructor() { }

  print(html: string, styles?: string, title?: string, orientation = "portrait" as "portrait" | "landscape", size = 'A4' as 'A4' | '4x6', addHeader = false) {
    var myWindow = window.open('', 'PRINT');
    myWindow!.document.write('<html><head><title></title>');
    myWindow!.document.write('</head><body>');
    myWindow!.document.write(`<style>
    * {
        -webkit-print-color-adjust: exact !important;
        color-adjust: exact !important;
      }
      @page {
        size: ${size == '4x6' ? "4in 6in" : size} ${orientation};
      }
      a{
        text-decoration: none;
      }
      `)
    if (styles)
      myWindow!.document.write(styles);
    myWindow!.document.write('</style>');
    //this.includeHeader(addHeader, myWindow);
    this.includeTitle(title !== undefined ?title:'', myWindow !== null? myWindow: new Window());
    myWindow!.document.write(html);
    myWindow!.document.write('</body></html>');
    myWindow!.document.close();
    myWindow!.focus();
    myWindow!.print();
    myWindow!.onafterprint = () => myWindow!.close();
  }

  private includeTitle(title: string, myWindow: Window) {
    if (title) {
      myWindow!.document.write(`
      <table style="all: none;width:100%" border="0">
        <tr>
        <td align="center">
        <span style="font-size:24px;font-weight:bold">${title}</span>
        </td>
        </tr>
        </table>
        <br>
        `);
    }
  }

  private includeHeader(addHeader: boolean, myWindow: Window) {
    if (addHeader) {
      myWindow!.document.write(`
      <table style="border-collapse: collapse;width: 100%;" border="0">
      <tr>
      <td align="center" colspan="2">
      <img src="assets/img/logo.png" height="60px">
      </td>
      <td colspan="6">
      Office No. 1234, ABC Tower, Business Bay, <br>
      Dubai, UAE <br>
      +971521234567 | www.ucsexpresss.com
      </td>
      </tr>
      </table>
      <br>
      `);
    }
  }
}
