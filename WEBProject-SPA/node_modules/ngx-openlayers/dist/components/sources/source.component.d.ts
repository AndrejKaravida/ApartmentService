/// <reference types="openlayers" />
import { OnDestroy } from '@angular/core';
import { source } from 'openlayers';
import { LayerComponent } from '../layers';
import AttributionLike = ol.AttributionLike;
import { SourceRasterComponent } from './raster.component';
export declare class SourceComponent implements OnDestroy {
    protected host: LayerComponent;
    protected raster: SourceRasterComponent;
    instance: source.Source;
    componentType: string;
    attributions: AttributionLike;
    constructor(host: LayerComponent, raster?: SourceRasterComponent);
    ngOnDestroy(): void;
    protected _register(source: source.Source): void;
}
