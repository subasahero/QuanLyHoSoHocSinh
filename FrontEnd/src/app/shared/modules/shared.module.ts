import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NgZorroAntdModule } from 'ng-zorro-antd';
import { ChartModule} from '@syncfusion/ej2-angular-charts';
import { CategoryService, BarSeriesService, ColumnSeriesService, LineSeriesService, LegendService,
DataLabelService, MultiLevelLabelService, SelectionService} from '@syncfusion/ej2-angular-charts';

@NgModule({
    imports: [
        CommonModule,
        NgZorroAntdModule,
        FormsModule,
        ReactiveFormsModule,
        ChartModule
    ],
    declarations: [
    ],
    exports: [CommonModule,
        NgZorroAntdModule,
        FormsModule,
        ReactiveFormsModule,
        ChartModule
    ],
    providers: [
        CategoryService, BarSeriesService, ColumnSeriesService, LineSeriesService, LegendService,
        DataLabelService, MultiLevelLabelService, SelectionService
    ]
})
export class SharedModule { }
