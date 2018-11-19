import { LogsService } from 'src/app/services';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-actions-report',
  templateUrl: './actions-report.component.html',
  styleUrls: ['./actions-report.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class ActionsReportComponent implements OnInit {
  public dateFormat: String = "dd-MM-yyyy";
  public value: Date;
  public rangePicked: boolean;
  public logs: any[];

  constructor(
    private logsService: LogsService
  ) { }

  ngOnInit() {
  }

  public getLogs() {
    console.log(this.value);
    let { from, to } = this.parseDates();
    this.logsService.getLogs(from, to)
      .then(response => {
        this.logs = response;
      })
      .catch(error => {
        alert(error);
        console.log(error);
      });
  }

  private parseDates() {
    let from = this.value[0].toISOString().substring(0, 10).replace('-', "").replace('-', "");
    let to = this.value[1].toISOString().substring(0, 10).replace('-', "").replace('-', "");
    return { from, to };
  }
}
