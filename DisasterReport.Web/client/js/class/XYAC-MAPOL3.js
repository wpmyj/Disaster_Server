(function (root, factory) {
    if (typeof exports === "object") {
        module.exports = factory();
    } else {
        root.XYACMapOl3 = factory();
    }
}(this, function () {
    if (typeof XYACMapOl3 === 'undefined') {
        var XYACMapOl3 = {};
    } else if (typeof XYACMapOl3 === 'object') {
        XYACMapOl3 = XYACMapOl3;
    }

    // 是否调试模式
    XYACMapOl3.ISDEBUG = true;

    // 真实的openlayer map对象， 私有属性
    var MapOl = null;

    // 全局LayerSet 图层集合
    var G_Layers = {};

    /**
     * @name 获取四川天地图layer
     * 
     * @param {any} urla 天地图路径
     * @returns
     */
    XYACMapOl3.getSCTdtLayer = function (urla) {
        var url = urla;
        var projection = ol.proj.get("EPSG:4326");
        var projectionExtent = [-180, -90, 180, 90];
        var maxResolution = (ol.extent.getWidth(projectionExtent) / (256 * 2));
        var resolutions = new Array(18);
        var z;
        for (z = 0; z < 18; ++z) {
            resolutions[z] = maxResolution / Math.pow(2, z);
        }
        var tileOrigin = ol.extent.getTopLeft(projectionExtent);
        var layer = new ol.layer.Tile({
            extent: [-180, -90, 180, 90],
            source: new ol.source.TileImage({
                tileUrlFunction: function (tileCoord) {
                    var z = tileCoord[0] + 1;
                    var x = tileCoord[1];
                    var y = -tileCoord[2] - 1;
                    var n = Math.pow(2, z + 1);
                    x = x % n;
                    if (x * n < 0) {
                        x = x + n;
                    }
                    return url.replace('{z}', z.toString())
                        .replace('{y}', y.toString())
                        .replace('{x}', x.toString());
                },
                projection: projection,
                tileGrid: new ol.tilegrid.TileGrid({
                    origin: tileOrigin,
                    resolutions: resolutions,
                    tileSize: 256
                })
            })
        });
        return layer;
    };

    /**
     * @name Map类
     * 
     * @param {any} options
     */
    XYACMapOl3.Map = function (options) {
        var _this = this;
        _this.Config = $.extend({}, _this.defaultsConfig, options || {});

        MapOl = new ol.Map({
            controls: ol.control.defaults({
                attribution: _this.Config.attribution
            }),
            target: _this.Config.target,
            layers: _this.Config.layers,
            view: new ol.View({
                center: _this.Config.center,
                zoom: _this.Config.zoom,
                projection: _this.Config.projection,
                minZoom: _this.Config.minZoom
            })
        });

        // ToolBar初始
        _this.bindToolBar();

        return _this;
    };

    // 四川天地图
    var tian_di_tu_road_layer = XYACMapOl3.getSCTdtLayer(
        'http://www.scgis.net.cn/iMap/iMapServer/DefaultRest/services/sctilemap/tile/{z}/{y}/{x}?returnTipTile=false&amp;token=t0eYFNOSM-gP6rG3RfE6Kf52qNPg1UEySyzyK8ajM_q4ZoqQrGaO_0pkd36FsRzX'
    );

    // 地图默认配置
    XYACMapOl3.Map.prototype.defaultsConfig = {
        attribution: false,
        target: 'xyacmap',
        layers: [tian_di_tu_road_layer],
        center: [104, 30.5],
        zoom: 9,
        projection: 'EPSG:4326',
        minZoom: 6,
        toolBar: {
            // ['放大', '缩小', '测距离', '侧面积', '全图']}
            enlarge: '#Map-ToolBar-enlarge',
            lessen: '#Map-ToolBar-lessen',
            meDistance: '#Map-ToolBar-meDistance',
            meArear: '#Map-ToolBar-meArear',
            allPicture: '#Map-ToolBar-allPicture'
        },
        popupSet: {},    // Overlay集合
    };

    /**
     * @name 注册toolbar按钮事件
     */
    XYACMapOl3.Map.prototype.bindToolBar = function () {

    };

    /**
     * @name MapOL3 addPopupLayer 增加一个popup标注容器
     * @description 主要用于对地图上people-icon点击后的信息显示
     * @param {Object} options 用户传参设置
     * @paramVal options.container   popup弹窗层容器 CSS选择器
     * @paramVal options.closer      popup弹窗层关闭按钮 CSS选择器
     * @paramVal options.content     popup弹窗层内容容器 CSS选择器
     * @paramVal options.id          popup弹窗层id
     * @version 0.0.1
     * @author zxy
     */
    XYACMapOl3.Map.prototype.addPopupLayer = function (options) {
        var _this = this;

        if (options && options.container && options.content && options.closer && options.id) {

            // 实现popup的html元素
            var container = options.container ? document.querySelector(options.container) : document.querySelector('#popup-container');
            var closer = options.closer ? document.querySelector(options.closer) : document.querySelector('#popup-closer');
            var content = options.content ? document.querySelector(options.content) : document.querySelector('#popup-content');
        } else {
            throw new Error("参数配置不全");
        }

        // 判读是否已经有此popup
        if (typeof _this.Config.popupSet[options.id] !== 'undefined') {
            throw new Error("不能重复添加相同id的popup");
        }

        // 在closer上保存此容器id
        closer.dataset.popupid = options.id;

        //在地图容器中创建一个Overlay
        _this.Config.popupSet[options.id] = new ol.Overlay(
            ({
                element: container,
                autoPan: true,
                positioning: 'bottom-center',
                stopEvent: true,    // 默认阻止冒泡
                autoPanAnimation: {
                    duration: 250
                }
            })
        );

        // 保存对应的content容器
        // this.popContent[options.container] = content;
        // 设置popup的内容到此容器属性上
        _this.Config.popupSet[options.id].popupid = options.id;

        _this.Config.popupSet[options.id].popContent = content;

        _this.Config.popupSet[options.id].show = function (center) {
            _this.Config.popupSet[this.popupid].popContent.parentNode.style.display = 'block';
            this.setPosition(center);
            return this;
        };

        _this.Config.popupSet[options.id].hidden = function () {
            _this.Config.popupSet[this.popupid].popContent.parentNode.style.display = 'none';
            this.setPosition(undefined);
            return this;
        };

        _this.Config.popupSet[options.id].setContent = function (content) {
            this.popContent.innerHTML = content;
            return this;
        };

        // 添加到地图里
        MapOl.addOverlay(_this.Config.popupSet[options.id]);

        /**
        * 添加关闭按钮的单击事件（隐藏popup）
        * @return {boolean} false 取消冒泡
        */
        closer.onclick = function (e) {
            e = e || window.event;
            this.blur();
            e.stopPropagation ? e.stopPropagation() : e.cancelBubble = true;

            _this.Config.popupSet[this.dataset.popupid].setPosition(undefined);  //未定义popup位置-隐藏

            return false;
        };

        return _this;
    };

    /**
     * @name 显示popup
     * 
     * @param {any} options
     * @paramVal options.id                     popup弹窗层id
     * @paramVal options.center [lng, lat];     弹窗显示位置
     */
    XYACMapOl3.Map.prototype.showPopup = function (options) {
        this.Config.popupSet[options.id].popContent.parentNode.style.display = 'block';
        this.Config.popupSet[options.id].setPosition(options.center);  //未定义popup位置-隐藏
        return this;
    };

    /**
     * @name 隐藏popup
     * 
     * @param {string | id} id  popup弹窗id 如果为空则隐藏所有的popup
     */
    XYACMapOl3.Map.prototype.hiddenPopup = function (id) {

        if (typeof id === 'undefined') {
            for(var popup in this.Config.popupSet) {
                this.Config.popupSet[popup].popContent.parentNode.style.display = 'none';
                this.Config.popupSet[popup].setPosition(undefined);
            }
            return this;
        }

        this.Config.popupSet[id].popContent.parentNode.style.display = 'none';
        this.Config.popupSet[id].setPosition(undefined);

        return this;
    };

    /**
     * @name 设置popup的内容
     * 
     * @param {any} options
     * @paramVal options.id         popup弹窗层id
     * @paramVal options.content    popup弹窗层内容innerHTML
     * @paramVal options.callback   设置内容后的回掉函数
     */
    XYACMapOl3.Map.prototype.setPopupContent = function (options) {
        try {
            this.Config.popupSet[options.id].popContent.innerHTML = options.content;
            options.callback && options.callback instanceof Function ? callback() : '';
            return this;
        } catch (error) {
            console.log(error);
        }
    };

    /**
     * @name 增加一个图层
     * 
     * @param {Object} options
     * @paramVal options.id     layer图层id
     * @paramVal options.isReturnBefore 是否返回之前存在的图层
     */
    XYACMapOl3.Map.prototype.addLayer = function (options) {
        var _this = this;
        if (!options.id || typeof G_Layers[options.id] !== 'undefined') {
            if (options.isReturnBefore && options.isReturnBefore === true) {
                return new _this.getLayer(options.id);
            }
            throw new Error('图层id不能为空、或图层id已被使用');
        }

        var _source = new ol.source.Vector();

        var _layer = new ol.layer.Vector({
            source: _source
        });

        G_Layers[options.id] = _layer;
        MapOl.addLayer(_layer);

        return new XYACMapOl3.Layer({
            id: options.id,
            layer: _layer
        }, true);
    };

    /**
     * @name 得到一个图层
     * @param {string} id 图层id
     */
    XYACMapOl3.Map.prototype.getLayer = function (id) {
        return new XYACMapOl3.Layer({
            id: id
        }, true);
    };

    /**
     * @name 得到地图视图
     * 
     */
    XYACMapOl3.Map.prototype.getView = function () {
        return new XYACMapOl3.View();
    };

    /**
     * @name View 类
     * 
     * @param {Object} options
     */
    XYACMapOl3.View = function (options) {
        var _this = this;
        var view = MapOl.getView();
        _this.Config = $.extend({}, {
            projection: view.getProjection(),
            resolution: view.getResolution()
        }, options || {});
        return this;
    };

    /**
     * @name MapOL3 flytoCenter 飞行效果移动到center位置
     * @description 主要用对地图的移动
     * @param options 用户传参设置
     * @paramVal {Number} options.duration 移动持续间
     * @paramVal {Array} options.center 平面坐标点经过经纬度转化后的坐标点 fromLonLat()
     * @version 0.0.2
     * @author zxy
     */
    XYACMapOl3.View.prototype.flytoCenter = function (options) {
        var _this = this;
        var duration = 1500;            //持续时间（毫秒）
        var start = +new Date();
        var view = MapOl.getView();

        //移动效果
        var pan = ol.animation.pan({
            duration: duration,         //设置持续时间
            source: /** @type {ol.Coordinate} */(view.getCenter()),
            start: start
        });

        //反弹效果
        var bounce = ol.animation.bounce({
            duration: duration,         //设置持续时间
            resolution: 4 * view.getResolution(),  //4倍分辨率
            start: start
        });

        MapOl.beforeRender(pan, bounce);    //地图渲染前设置动画效果(pan+bounce)
        view.setCenter(options.center);     //定位

        return _this;
    };

    /**
     * @name MapOL3 setCenter 定位到指定的center
     * @description 主要用对地图的移动
     * @param {any} options
     * @paramVal {Number} options.zoom 缩放级别
     * @paramVal {Array} options.center 平面坐标点经过经纬度转化后的坐标点 fromLonLat()
     */
    XYACMapOl3.View.prototype.setCenter = function (options) {
        var _this = this;
        var view = MapOl.getView();

        view.setCenter(options.center);     //定位
        view.setZoom(options.zoom ? options.zoom : view.getZoom());

        return _this;
    };

    /**
     * @name Layer类
     * 
     * @param {Object} options
     * @param {boolean} opt_isVector ol.layer.Vector
     */
    XYACMapOl3.Layer = function (options, opt_isVector) {

        var _this = this;
        if (opt_isVector && opt_isVector === true) {     // 说明options是一个<ol.layer.Vector>类型

            _this.id = options.id;                       // 每一个对象保存对应图层id
        } else {
            if (!options.id || typeof G_Layers[options.id] !== 'undefined') {
                throw new Error('图层id不能为空、或图层id已被使用');
            }
            var _source = new ol.source.Vector();

            var _layer = new ol.layer.Vector({
                source: _source
            });

            _this.id = options.id;
            G_Layers[options.id] = _layer;
            MapOl.addLayer(_layer);
        }

        return _this;
    };

    /**
     * @name 在当前对应的layer图层上增加Features
     * 
     * @param {Object || Array} options
     * @paramVal {string} options.type => point, line
     * @paramVal {any} options.attr => 自定义属性
     * @paramVal {Array} options.path => geometry路径 point=>[lng, lat], line=>[[lng, lat],[lng, lat]...]
     */
    XYACMapOl3.Layer.prototype.addFeatures = function (options) {
        var _this = this;

        if (typeof options === 'object') {

            if (Object.prototype.toString.call(options) !== '[object Array]') {  // 如果不是数组
                options = [options];
            }
        } else {
            throw new Error('参数传递有数');
        }

        var _features = [];
        for (var i = 0; i < options.length; i++) {

            if (options[i].type === 'point') {  // 判断是否有多个点
                if (typeof options[i].path[0] === 'object' && Object.prototype.toString.call(options[i].path[0]) === '[object Array]') {
                    for (var j = 0; j < options[i].path.length; j++) {
                        var _feature = new ol.Feature({
                            geometry: new ol.geom.Point(options[i].path[j]),
                            attr: options[i].attr || {}
                        });
                        _features.push(_feature);
                    }
                } else {
                    var _feature = new ol.Feature({
                        geometry: new ol.geom.Point(options[i].path),
                        attr: options[i].attr || {}
                    });
                    _features.push(_feature);
                }
            } else if (options[i].type === 'line') {
                var _feature = new ol.Feature({
                    geometry: new ol.geom.LineString(options[i].path),
                    attr: options[i].attr || {}
                });
                _features.push(_feature);
            }

        }

        G_Layers[_this.id].getSource().addFeatures(_features);

        return _this;
    };

    /**
     * @name 清除当前layer上所有的Features
     */
    XYACMapOl3.Layer.prototype.clearAll = function () {
        var _this = this;

        G_Layers[_this.id].getSource().clear();

        return _this;
    };

    /**
     * @name 设置对应图层上Features的样式
     * 
     * @param {ol.style.Style | Array.<ol.style.Style> | ol.FeatureStyleFunction | ol.StyleFunction} 传递ol3的样式对象
     */
    XYACMapOl3.Layer.prototype.setStyle = function (style) {
        var _this = this;
        var _features = G_Layers[_this.id].getSource().getFeatures();

        for (var i = 0; i < _features.length; i++) {
            _features[i].setStyle(new ol.style.Style(style));
        }

        // G_Layers[_this.id].getSource().refresh();

        return _this;
    };

    return XYACMapOl3;
}));