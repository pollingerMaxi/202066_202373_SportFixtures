import { LogsService } from 'src/app/services';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-actions-report',
  templateUrl: './actions-report.component.html',
  styleUrls: ['./actions-report.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class ActionsReportComponent implements OnInit {
  public minDate: Date = new Date('11/15/2018');
  public maxDate: Date = new Date('11/20/2018');
  value: Date;

  constructor(
    private logsService: LogsService
  ) { }

  ngOnInit() {
  }

  public getLogs() {
    this.logsService.getLogs()
      .then(response => {
        console.log(response);

      })
      .catch(error => {
        console.log(error);

      });;
  }
}
