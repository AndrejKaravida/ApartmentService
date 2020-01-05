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
var layerimage_component_1 = require("../layers/layerimage.component");
var source_component_1 = require("./source.component");
var SourceRasterComponent = (function (_super) {
    __extends(SourceRasterComponent, _super);
    function SourceRasterComponent(layer) {
        var _this = _super.call(this, layer) || this;
        _this.beforeOperations = new core_1.EventEmitter();
        _this.afterOperations = new core_1.EventEmitter();
        _this.sources = [];
        return _this;
    }
    SourceRasterComponent.prototype.ngAfterContentInit = function () {
        var _this = this;
        this.instance = new openlayers_1.source.Raster(this);
        this.instance.on('beforeoperations', function (event) { return _this.beforeOperations.emit(event); });
        this.instance.on('afteroperations', function (event) { return _this.afterOperations.emit(event); });
        this._register(this.instance);
    };
    return SourceRasterComponent;
}(source_component_1.SourceComponent));
SourceRasterComponent.decorators = [
    { type: core_1.Component, args: [{
                selector: 'aol-source-raster',
                template: "<ng-content></ng-content>",
                providers: [
                    {
                        provide: source_component_1.SourceComponent,
                        useExisting: core_1.forwardRef(function () { return SourceRasterComponent; })
                    }
                ]
            },] },
];
/** @nocollapse */
SourceRasterComponent.ctorParameters = function () { return [
    { type: layerimage_component_1.LayerImageComponent, decorators: [{ type: core_1.Host },] },
]; };
SourceRasterComponent.propDecorators = {
    'operation': [{ type: core_1.Input },],
    'threads': [{ type: core_1.Input },],
    'lib': [{ type: core_1.Input },],
    'operationType': [{ type: core_1.Input },],
    'beforeOperations': [{ type: core_1.Output },],
    'afterOperations': [{ type: core_1.Output },],
};
exports.SourceRasterComponent = SourceRasterComponent;
//# sourceMappingURL=raster.component.js.map