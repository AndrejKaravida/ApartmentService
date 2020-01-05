/// <reference types="openlayers" />
import { AfterContentInit } from '@angular/core';
import { source, Feature, geom } from 'openlayers';
import { LayerVectorComponent } from '../layers';
import { SourceComponent } from './source.component';
import { SourceVectorComponent } from './vector.component';
export declare class SourceClusterComponent extends SourceComponent implements AfterContentInit {
    instance: source.Cluster;
    distance: number;
    geometryFunction?: ((feature: Feature) => geom.Point);
    sourceVectorComponent: SourceVectorComponent;
    source: source.Vector;
    constructor(layer: LayerVectorComponent);
    ngAfterContentInit(): void;
}
