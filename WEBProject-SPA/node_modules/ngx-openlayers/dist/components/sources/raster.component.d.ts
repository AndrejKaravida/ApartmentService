/// <reference types="openlayers" />
import { AfterContentInit, EventEmitter } from '@angular/core';
import { RasterOperation, RasterOperationType, source } from 'openlayers';
import { LayerImageComponent } from '../layers/layerimage.component';
import { SourceComponent } from './source.component';
export declare class SourceRasterComponent extends SourceComponent implements AfterContentInit {
    instance: source.Raster;
    operation?: RasterOperation;
    threads?: number;
    lib?: Object;
    operationType?: RasterOperationType;
    beforeOperations: EventEmitter<source.RasterEvent>;
    afterOperations: EventEmitter<source.RasterEvent>;
    sources: source.Source[];
    constructor(layer: LayerImageComponent);
    ngAfterContentInit(): void;
}
