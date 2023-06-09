/*
THIS IS A GENERATED/BUNDLED FILE BY ESBUILD
if you want to view the source, please visit the github repository of this plugin
*/

var __defProp = Object.defineProperty;
var __getOwnPropDesc = Object.getOwnPropertyDescriptor;
var __getOwnPropNames = Object.getOwnPropertyNames;
var __hasOwnProp = Object.prototype.hasOwnProperty;
var __export = (target, all) => {
  for (var name in all)
    __defProp(target, name, { get: all[name], enumerable: true });
};
var __copyProps = (to, from, except, desc) => {
  if (from && typeof from === "object" || typeof from === "function") {
    for (let key of __getOwnPropNames(from))
      if (!__hasOwnProp.call(to, key) && key !== except)
        __defProp(to, key, { get: () => from[key], enumerable: !(desc = __getOwnPropDesc(from, key)) || desc.enumerable });
  }
  return to;
};
var __toCommonJS = (mod) => __copyProps(__defProp({}, "__esModule", { value: true }), mod);
var __async = (__this, __arguments, generator) => {
  return new Promise((resolve, reject) => {
    var fulfilled = (value) => {
      try {
        step(generator.next(value));
      } catch (e) {
        reject(e);
      }
    };
    var rejected = (value) => {
      try {
        step(generator.throw(value));
      } catch (e) {
        reject(e);
      }
    };
    var step = (x) => x.done ? resolve(x.value) : Promise.resolve(x.value).then(fulfilled, rejected);
    step((generator = generator.apply(__this, __arguments)).next());
  });
};

// src/main.ts
var main_exports = {};
__export(main_exports, {
  default: () => ImageLayoutsPlugin
});
module.exports = __toCommonJS(main_exports);
var import_obsidian = require("obsidian");
var regexWiki = /\[\[([^\]]+)\]\]/;
var regexParenthesis = /\((.*?)\)/;
var regexWikiGlobal = /\[\[([^\]]*)\]\]/g;
var regexMdGlobal = /\[([^\]]*)\]\(([^\(]*)\)/g;
var layoutImages = {
  "a": 2,
  "b": 2,
  "c": 2,
  "d": 3,
  "e": 3,
  "f": 4,
  "g": 4,
  "h": 3,
  "i": 4
};
var addImageFromLink = (link, sourcePath, parent, plugin) => {
  var destFile = app.metadataCache.getFirstLinkpathDest(link, sourcePath);
  if (destFile) {
    const img = parent.createEl("img");
    img.src = plugin.app.vault.adapter.getResourcePath(destFile.path);
  }
};
var addExternalImage = (link, parent) => {
  const img = parent.createEl("img");
  img.src = link;
};
var addPlaceHolder = (widthXHeight, parent) => {
  widthXHeight = widthXHeight != null ? widthXHeight : "640x480";
  const img = parent.createEl("img");
  img.src = `https://via.placeholder.com/${widthXHeight}`;
};
var renderLayout = (images, layout, sourcePath, parent, plugin) => {
  const layoutImagesCount = layoutImages[layout];
  if (images.length < layoutImagesCount) {
    for (let i = images.length; i < layoutImagesCount; i++) {
      images.push({ type: "placeholder" });
    }
  }
  if (images.length > layoutImagesCount) {
    images = images.slice(0, layoutImagesCount);
  }
  const div = parent.createEl("div", { cls: `image-layouts-grid image-layouts-layout-${layout}` });
  images.forEach((image, idx) => {
    const imgdiv = div.createEl("div", { cls: `image-layouts-image-${idx}` });
    if (image.type === "local") {
      addImageFromLink(image.link, sourcePath, imgdiv, plugin);
    } else if (image.type === "external") {
      console.log(image.link);
      addExternalImage(image.link, imgdiv);
    } else if (image.type === "placeholder") {
      addPlaceHolder("640x480", imgdiv);
    }
  });
};
var renderMasonryLayout = (images, columns, sourcePath, parent, plugin) => {
  const div = parent.createEl("div", { cls: `image-layouts-masonry-grid-${columns}` });
  const columnDivs = [];
  for (let i = 0; i < columns; i++) {
    const colDiv = div.createEl("div", { cls: `image-layouts-masonry-column` });
    columnDivs.push(colDiv);
  }
  images.forEach((image, idx) => {
    const colIdx = idx % columns;
    const imgdiv = columnDivs[colIdx].createEl("div", { cls: `image-layouts-masonry-image-${idx}` });
    if (image.type === "local") {
      addImageFromLink(image.link, sourcePath, imgdiv, plugin);
    } else if (image.type === "external") {
      console.log(image.link);
      addExternalImage(image.link, imgdiv);
    }
  });
};
var getImages = (source) => {
  const lines = source.split("\n").filter((row) => row.startsWith("!"));
  const images = lines.map((line) => getImageFromLine(line));
  return images.filter((image) => image !== null);
};
var getImageFromLine = (line) => {
  var _a, _b;
  if (line.match(regexMdGlobal)) {
    const link = (_a = line.match(regexParenthesis)) == null ? void 0 : _a[1];
    if (link) {
      return { type: "external", link };
    }
  } else if (line.match(regexWikiGlobal)) {
    const link = (_b = line.match(regexWiki)) == null ? void 0 : _b[1];
    if (link) {
      return {
        type: "local",
        link
      };
    }
  }
  return null;
};
var ImageLayoutsPlugin = class extends import_obsidian.Plugin {
  onload() {
    return __async(this, null, function* () {
      Object.keys(layoutImages).forEach((layout) => {
        this.registerMarkdownCodeBlockProcessor(`image-layout-${layout}`, (source, el, ctx) => {
          const images = getImages(source);
          renderLayout(images, layout, ctx.sourcePath, el, this);
        });
      });
      for (let i = 2; i <= 6; i++) {
        this.registerMarkdownCodeBlockProcessor(`image-layout-masonry-${i}`, (source, el, ctx) => {
          const images = getImages(source);
          renderMasonryLayout(images, i, ctx.sourcePath, el, this);
        });
      }
    });
  }
};
