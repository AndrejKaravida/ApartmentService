"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var openlayers_1 = require("openlayers");
var layers_1 = require("../layers");
var source_component_1 = require("./source.component");
var SourceTileUTFGridComponent = (function (_super) {
    __extends(SourceTileUTFGridComponent, _super);
    function SourceTileUTFGridComponent(layer) {
        return _super.call(this, layer) || this;
    }
    SourceTileUTFGridComponent.prototype.ngOnInit = function () {
        this.instance = new openlayers_1.source.TileUTFGrid(this);
        this.host.instance.setSource(this.instance);
    };
    return SourceTileUTFGridComponent;
}(source_component_1.SourceComponent));
SourceTileUTFGridComponent.decorators = [
    { type: core_1.Component, args: [{
                selector: 'aol-source-tileutfgrid',
                template: "<ng-content></ng-content>",
                providers: [
                    { provide: source_component_1.SourceComponent, useExisting: core_1.forwardRef(function () { return SourceTileUTFGridComponent; }) }
                ]
            },] },
];
/** @nocollapse */
SourceTileUTFGridComponent.ctorParameters = function () { return [
    { type: layers_1.LayerTileComponent, decorators: [{ type: core_1.Host },] },
]; };
SourceTileUTFGridComponent.propDecorators = {
    'tileJSON': [{ type: core_1.Input },],
};
exports.SourceTileUTFGridComponent = SourceTileUTFGridComponent;
//# sourceMappingURL=tileutfgrid.component.js.map