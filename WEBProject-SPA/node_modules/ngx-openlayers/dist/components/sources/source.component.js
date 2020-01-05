"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var SourceComponent = (function () {
    function SourceComponent(host, raster) {
        this.host = host;
        this.raster = raster;
        this.componentType = 'source';
    }
    SourceComponent.prototype.ngOnDestroy = function () {
        if (this.host) {
            this.host.instance.setSource(null);
        }
        if (this.raster) {
            this.raster.sources = [];
        }
    };
    SourceComponent.prototype._register = function (source) {
        if (this.host) {
            this.host.instance.setSource(source);
        }
        if (this.raster) {
            this.raster.sources = [source];
        }
    };
    return SourceComponent;
}());
SourceComponent.propDecorators = {
    'attributions': [{ type: core_1.Input },],
};
exports.SourceComponent = SourceComponent;
//# sourceMappingURL=source.component.js.map