/// <reference types="openlayers" />
import { OnInit } from '@angular/core';
import { source } from 'openlayers';
import { LayerTileComponent } from '../layers';
import { SourceComponent } from './source.component';
export declare class SourceTileUTFGridComponent extends SourceComponent implements OnInit {
    instance: source.TileUTFGrid;
    tileJSON: JSON;
    constructor(layer: LayerTileComponent);
    ngOnInit(): void;
}
