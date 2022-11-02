var CavaniAjax = CavaniAjaxObject;
var CavaniBody = jQuery("body");
var CavaniWrapper = jQuery(".cavani-fn-wrapper");
(function ($) {
  "use strict";
  var CavaniInit = {
    pageNumber: 1,
    init: function () {
      this.cursor();
      this.blog_info();
      this.minHeightForPages();
      this.url_fixer();
      this.projectCategoryFitler();
      this.portfolioFilter();
      this.hamburgerOpener__Mobile();
      this.submenu__Mobile();
      this.imgToSVG();
      this.isotopeMasonry();
      this.dataFnBgImg();
      this.dataFnStyle();
      this.estimateWidgetHeight();
      this.categoryHook();
      this.toTopJumper();
      this.rating();
      this.fixedTotopScroll();
      this.prev_next_posts();
      this.widget__pages();
      this.widget__archives();
      this.portfolioContentHeight();
      this.inputCheckBoxInComment();
      this.rightSidebar();
      this.menuHover();
      this.filterHover();
      this.blogPostsImageMoving();
      this.reverseMenu();
      this.portfolioSlider();
      this.portfolioPopup();
      this.leftContentEffects();
    },
    leftContentEffects: function () {
      $(".glitch").mgGlitch({
        destroy: false,
        glitch: true,
        scale: true,
        blend: true,
        blendModeType: "hue",
        glitch1TimeMin: 200,
        glitch1TimeMax: 400,
        glitch2TimeMin: 10,
        glitch2TimeMax: 100,
      });
      if ($("#ripple").length) {
        $("#ripple").ripples({
          resolution: 500,
          dropRadius: 20,
          perturbance: 0.04,
        });
      }
    },
    portfolioPopup: function () {
      $(".cavani_popup_youtube, .cavani_popup_vimeo").each(function () {
        $(this).magnificPopup({
          disableOn: 700,
          type: "iframe",
          mainClass: "mfp-fade",
          removalDelay: 160,
          preloader: false,
          fixedContentPos: false,
        });
      });
      $(".cavani_popup_soundcloud").magnificPopup({
        type: "image",
        gallery: { enabled: true },
      });
    },
    portfolioSlider: function () {
      var section = $(".cavani_fn_content .overlay_slider .swiper-container");
      section.each(function () {
        var element = $(this);
        var transform = "Y";
        var direction = "horizontal";
        var interleaveOffset = 0.5;
        if (direction === "horizontal") {
          transform = "X";
        }
        var rate = 1;
        if ($("body").hasClass("rtl")) {
          rate = -1;
        }
        var mainSliderOptions = {
          loop: true,
          speed: 1500,
          autoplay: { delay: 5000, disableOnInteraction: false },
          navigation: {
            nextEl: element.closest(".overlay_slider").find(".next"),
            prevEl: element.closest(".overlay_slider").find(".prev"),
          },
          slidesPerView: 1,
          direction: direction,
          loopAdditionalSlides: 10,
          watchSlidesProgress: true,
          on: {
            init: function () {
              this.autoplay.stop();
            },
            imagesReady: function () {
              this.autoplay.start();
            },
            progress: function () {
              var swiper = this;
              for (var i = 0; i < swiper.slides.length; i++) {
                var slideProgress = swiper.slides[i].progress,
                  innerOffset = swiper.width * interleaveOffset,
                  innerTranslate = slideProgress * innerOffset * rate;
                $(swiper.slides[i])
                  .find(".main_image")
                  .css({
                    transform:
                      "translate" + transform + "(" + innerTranslate + "px)",
                  });
              }
            },
            touchStart: function () {
              var swiper = this;
              for (var i = 0; i < swiper.slides.length; i++) {
                swiper.slides[i].style.transition = "";
              }
            },
            setTransition: function (speed) {
              var swiper = this;
              for (var i = 0; i < swiper.slides.length; i++) {
                swiper.slides[i].style.transition = speed + "ms";
                swiper.slides[i].querySelector(".main_image").style.transition =
                  speed + "ms";
              }
            },
          },
        };
        new Swiper(element, mainSliderOptions);
      });
    },
    reverseMenu: function () {
      $(".cavani_fn_main_nav ul").each(function () {
        var e = $(this),
          p = e.parent(),
          w = p.offset().left + e.width(),
          W = $(".cavani_fn_content").width();
        if (w > W) {
          e.addClass("reverse");
        }
      });
    },
    rightSidebar: function () {
      var input = $('.cavani_fn_rsidebar .search_wrap input[type="text"]');
      var className = "right-sidebar-opened";
      $(".cavani_fn_header .trigger,.cavani_fn_rsidebar .search_wrap .closer")
        .off()
        .on("click", function () {
          if (!CavaniWrapper.hasClass(className)) {
            input.focus();
            CavaniWrapper.addClass(className);
          } else {
            CavaniWrapper.removeClass(className);
          }
          return false;
        });
    },
    filterHover: function () {
      var nav = $(".cavani_fn_ajax_portfolio .filFter_wrapper");
      var ccc = nav.find(".ccc");
      var element = nav.find(".current");
      $(".cavani_fn_ajax_portfolio .posts_filter li").on(
        "mouseenter",
        function () {
          var e = $(this);
          CavaniInit.currentLink(nav, ccc, e);
        }
      );
      nav
        .on("mouseenter", function () {
          nav.addClass("hovered");
        })
        .on("mouseleave", function () {
          nav.removeClass("hovered");
          nav.find(".hovered").removeClass("hovered");
          element = nav.find(".current");
          if (!element.length) {
            ccc.css({ width: 0 });
            return false;
          }
          CavaniInit.currentLink(nav, ccc, element);
        });
      CavaniInit.currentLink(nav, ccc, element);
    },
    menuHover: function () {
      var nav = $(".cavani_fn_header .nav");
      var ccc = nav.find(".ccc");
      var element = nav.find(".current-menu-item");
      $(".cavani_fn_main_nav > li").on("mouseenter", function () {
        var e = $(this);
        CavaniInit.currentLink(nav, ccc, e);
      });
      nav
        .on("mouseenter", function () {
          nav.addClass("hovered");
        })
        .on("mouseleave", function () {
          nav.removeClass("hovered");
          nav.find(".hovered").removeClass("hovered");
          element = nav.find(".current-menu-item");
          if (element.closest(".current-menu-ancestor").length) {
            element = element.closest(".current-menu-ancestor");
          }
          if (!element.length) {
            ccc.css({ width: 0 });
            return false;
          }
          CavaniInit.currentLink(nav, ccc, element);
        });
      if (element.closest(".current-menu-ancestor").length) {
        element = element.closest(".current-menu-ancestor");
      }
      CavaniInit.currentLink(nav, ccc, element);
    },
    currentLink: function (nav, ccc, e) {
      if (!e.length) {
        return false;
      }
      e.addClass("hovered");
      e.siblings().removeClass("hovered");
      var left = e.offset().left;
      var width = e.outerWidth();
      var menuleft = nav.offset().left;
      ccc.css({
        left: left - menuleft + "px",
        width: width + "px",
        top: e.offset().top - nav.offset().top + "px",
      });
    },
    blogPostsImageMoving: function () {
      var wrapper = $(".cavani_fn_posts");
      var list = wrapper.find(".post_item");
      if (!$(".cavani_fn_moving_box").length) {
        $(".cavani_fn_posts").append(
          '<div class="cavani_fn_moving_box"></div>'
        );
      }
      var box = $(".cavani_fn_moving_box");
      list
        .on("mouseenter", function () {
          var element = $(this);
          var image = element.data("img");
          var ellOffset = element.offset().top;
          ellOffset -= $(".cavani_fn_header").height();
          if ($("#wpadminbar").length) {
            ellOffset -= $("#wpadminbar").height();
          }
          if (image === "") {
            box.removeClass("opened");
            return false;
          }
          box.addClass("opened");
          box.css({
            backgroundImage: "url(" + image + ")",
            top: ellOffset + "px",
          });
        })
        .on("mouseleave", function () {
          box.removeClass("opened");
        });
    },
    blog_info: function () {
      if ($(".blog_info").height() === 0) {
        $(".cavani_fn_comment").addClass("margin-no-top");
      }
      if ($(".wp-calendar-nav").length) {
        $(".wp-calendar-nav").each(function () {
          var e = $(this);
          if (!e.find("a").length) {
            e.remove();
          }
        });
      }
    },
    projectPopup: function () {
      $(".cavani_popup_gallery").each(function () {
        $(this).magnificPopup({
          delegate: "a.zoom",
          type: "image",
          gallery: { enabled: true },
          removalDelay: 300,
          mainClass: "mfp-fade",
        });
      });
      $(".cavani_popup_youtube, .cavani_popup_vimeo").each(function () {
        $(this).magnificPopup({
          disableOn: 700,
          type: "iframe",
          mainClass: "mfp-fade",
          removalDelay: 160,
          preloader: false,
          fixedContentPos: false,
        });
      });
      $(".cavani_popup_soundcloude").each(function () {
        $(this).magnificPopup({ type: "image", gallery: { enabled: true } });
      });
    },
    runPreloader: function () {
      var speed = 500,
        self = this;
      setTimeout(function () {
        self.preloader();
      }, speed);
    },
    preloader: function () {
      var isMobile = /Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(
        navigator.userAgent
      )
        ? true
        : false;
      var preloader = $(".cavani_fn_pageloader");
      if (!isMobile) {
        setTimeout(function () {
          preloader.addClass("fn_ready");
        }, 800);
        setTimeout(function () {
          preloader.remove();
        }, 2000);
      } else {
        preloader.remove();
      }
    },
    inputCheckBoxInComment: function () {
      if ($("p.comment-form-cookies-consent input[type=checkbox]").length) {
        $("p.comment-form-cookies-consent input[type=checkbox]")
          .wrap('<label class="fn_checkbox"></label>')
          .after("<span></span>");
      }
    },
    portfolioContentHeight: function () {
      var portfolio = $(".cavani_fn_portfolio_page .portfolio_content");
      if (portfolio.height() === 0) {
        portfolio.css({ display: "none" });
      }
    },
    minHeightForPages: function () {
      var windowH = $(window).height();
      var headerH = $(".cavani_fn_header").height();
      var footerH = $(".cavani_fn_footer").height();
      var searchH = $(".cavani_fn_rsidebar .search_wrap").height();
      var adminbarH = 0;
      if ($("#wpadminbar").length) {
        adminbarH = $("#wpadminbar").height();
      }
      var height = windowH - headerH - footerH - adminbarH + "px";
      $(".cavani_fn_content,.cavani_fn_page").css({
        maxHeight: height,
        height: height,
      });
      $(".cavani_fn_404,.cavani-fn-protected").css({ minHeight: height });
      $(".cavani_fn_rsidebar .sidebar_bottom").css({
        maxHeight: windowH - searchH - adminbarH + "px",
        height: windowH - searchH - adminbarH + "px",
      });
    },
    url_fixer: function () {
      $('a[href*="fn_ex_link"]').each(function () {
        var oldUrl = $(this).attr("href"),
          array = oldUrl.split("fn_ex_link/"),
          newUrl = CavaniAjax.siteurl + "/" + array[1];
        $(this).attr("href", newUrl);
      });
      if ($(".cavani-fn-protected").length) {
        $(".cavani_fn_pagein").css({ paddingTop: 0 });
      }
    },
    portfolioFilter: function () {
      var self = this;
      $(".cavani_fn_portfolio_page .fn_ajax_more a")
        .off()
        .on("click", function () {
          var thisButton = $(this);
          var more = thisButton.closest(".fn_ajax_more");
          var input = more.find("input");
          var abb = thisButton.closest(".cavani_fn_portfolio_page");
          var filter_page = parseInt(input.val());
          if (thisButton.hasClass("active")) {
            return false;
          }
          if (!abb.hasClass("go") && !more.hasClass("disabled")) {
            abb.addClass("go");
            var requestData = {
              action: "cavani_fn_ajax_portfolio",
              filter_page: filter_page,
              security: CavaniAjax.nonce,
            };
            $.ajax({
              type: "POST",
              url: CavaniAjax.ajax_url,
              cache: false,
              data: requestData,
              success: function (data) {
                var fnQueriedObj = $.parseJSON(data);
                var html = fnQueriedObj.data;
                var $grid = abb.find(".posts_list");
                var $items;
                $items = $(html);
                input.val(filter_page + 1);
                input.change();
                if (fnQueriedObj.disabled === "disabled") {
                  more.addClass("disabled");
                }
                $grid.append($items).isotope("appended", $items);
                setTimeout(function () {
                  $grid.isotope({
                    itemSelector: "li",
                    masonry: {},
                    stagger: 30,
                  });
                }, 500);
                self.dataFnBgImg();
                self.dataFnStyle();
                abb.removeClass("go");
              },
              error: function (xhr, textStatus, errorThrown) {
                abb.removeClass("go");
              },
            });
          }
          return false;
        });
    },
    projectCategoryFitler: function () {
      if ($().isotope) {
        var items = $(".cavani_fn_ajax_portfolio");
        items.each(function () {
          var thisItem = $(this);
          var list = thisItem.find(".posts_list");
          var filter = thisItem.find(".posts_filter");
          list.isotope({ itemSelector: "li", masonry: {}, stagger: 30 });
          filter
            .find("a")
            .off()
            .on("click", function () {
              var element = $(this);
              var selector = element.attr("data-filter");
              list = thisItem.find(".posts_list");
              filter.find("li").removeClass("current");
              element.parent().addClass("current");
              list.isotope({
                filter: selector,
                animationOptions: {
                  duration: 750,
                  easing: "linear",
                  queue: false,
                },
              });
              return false;
            });
        });
      }
    },
    cursor: function () {
      var myCursor = $(".marketify-cursor");
      if (myCursor.length) {
        if ($("body").length) {
          const e = document.querySelector(".cursor-inner"),
            t = document.querySelector(".cursor-outer");
          var n,
            i = 0,
            W = 0,
            intro = 0,
            o = !1;
          if ($(".cavani_fn_intro").length) {
            intro = 1;
          }
          var buttons =
            ".cavani_fn_header .trigger,.fn_cs_intro_testimonials .prev, .fn_cs_intro_testimonials .next, .fn_cs_swiper_nav_next, .fn_cs_swiper_nav_prev, .fn_dots, .swiper-button-prev, .swiper-button-next, .fn_cs_accordion .acc_head, .cavani_fn_popupshare .share_closer, .cavani_fn_header .fn_finder, .cavani_fn_header .fn_trigger, a, input[type='submit'], .cursor-link, button";
          var sliders = ".owl-carousel, .swiper-container, .cursor-link";
          (window.onmousemove = function (s) {
            o ||
              (t.style.transform =
                "translate(" + s.clientX + "px, " + s.clientY + "px)"),
              (e.style.transform =
                "translate(" + s.clientX + "px, " + s.clientY + "px)"),
              (n = s.clientY),
              (i = s.clientX);
          }),
            $("body").on("mouseenter", buttons, function () {
              e.classList.add("cursor-hover"), t.classList.add("cursor-hover");
            }),
            $("body").on("mouseleave", buttons, function () {
              ($(this).is("a") && $(this).closest(".cursor-link").length) ||
                (e.classList.remove("cursor-hover"),
                t.classList.remove("cursor-hover"));
            }),
            (e.style.visibility = "visible"),
            (t.style.visibility = "visible");
          CavaniBody.on("mouseenter", sliders, function () {
            e.classList.add("cursor-slider");
            t.classList.add("cursor-slider");
          }).on("mouseleave", sliders, function () {
            e.classList.remove("cursor-slider");
            t.classList.remove("cursor-slider");
          });
          CavaniBody.on("mousedown", sliders, function () {
            e.classList.add("mouse-down");
            t.classList.add("mouse-down");
          }).on("mouseup", sliders, function () {
            e.classList.remove("mouse-down");
            t.classList.remove("mouse-down");
          });
        }
      }
    },
    widget__archives: function () {
      $(".widget_archive li").each(function () {
        var e = $(this);
        var a = e.find("a").clone();
        CavaniBody.append('<div class="marketify_hidden_item"></div>');
        $(".marketify_hidden_item").html(e.html());
        $(".marketify_hidden_item").find("a").remove();
        var suffix = $(".marketify_hidden_item").html().match(/\d+/);
        $(".marketify_hidden_item").remove();
        suffix = parseInt(suffix);
        if (isNaN(suffix)) {
          return false;
        }
        suffix = '<span class="count">' + suffix + "</span>";
        e.html(a);
        e.append(suffix);
      });
    },
    prev_next_posts: function () {
      if ($(".cavani_fn_siblings")) {
        $(document).keyup(function (e) {
          if (e.key.toLowerCase() === "p") {
            var a = $(".cavani_fn_siblings").find("a.previous_project_link");
            if (a.length) {
              window.location.href = a.attr("href");
              return false;
            }
          }
          if (e.key.toLowerCase() === "n") {
            var b = $(".cavani_fn_siblings").find("a.next_project_link");
            if (b.length) {
              window.location.href = b.attr("href");
              return false;
            }
          }
        });
      }
    },
    fixedTotopScroll: function () {
      var totop = $(".cavani_fn_totop");
      var height = parseInt(totop.find("input").val());
      if (totop.length) {
        if ($(window).scrollTop() > height) {
          totop.addClass("scrolled");
        } else {
          totop.removeClass("scrolled");
        }
      }
    },
    categoryHook: function () {
      var self = this;
      var list = $(
        ".wp-block-archives li, .widget_cavani_custom_categories li, .widget_categories li, .widget_archive li"
      );
      list.each(function () {
        var item = $(this);
        if (item.find("ul").length) {
          item.addClass("has-child");
        }
      });
      var html = $(".cavani_fn_hidden.more_cats").html();
      var cats = $(
        ".widget_categories,.widget_archive,.widget_cavani_custom_categories"
      );
      if (cats.length) {
        cats.each(function () {
          var element = $(this);
          var limit = 3;
          element.find(".block_inner").append(html);
          var li = element.find("ul:not(.children) > li");
          if (li.length > limit) {
            var h = 0;
            li.each(function (i, e) {
              if (i < limit) {
                h += $(e).outerHeight(true, true);
              } else {
                return false;
              }
            });
            element.find("ul:not(.children)").css({ height: h + "px" });
            element
              .find(".cavani_fn_more_categories .fn_count")
              .html("(" + (li.length - limit) + ")");
          } else {
            element.addClass("all_active");
          }
        });
        self.categoryHookAction();
      }
    },
    categoryHookAction: function () {
      $(".cavani_fn_more_categories")
        .find("a")
        .off()
        .on("click", function () {
          var e = $(this);
          var limit = 3;
          var myLimit = limit;
          var parent = e.closest(".widget_block");
          var li = parent.find("ul:not(.children) > li");
          var liHeight = li.outerHeight(true, true);
          var h = liHeight * limit;
          var liLength = li.length;
          var speed = (liLength - limit) * 50;
          e.toggleClass("show");
          if (e.hasClass("show")) {
            myLimit = liLength;
            h = liHeight * liLength;
            e.find(".text").html(e.data("less"));
            e.find(".fn_count").html("");
          } else {
            e.find(".text").html(e.data("more"));
            e.find(".fn_count").html("(" + (liLength - limit) + ")");
          }
          var H = 0;
          li.each(function (i, e) {
            if (i < myLimit) {
              H += $(e).outerHeight(true, true);
            } else {
              return false;
            }
          });
          speed = speed > 300 ? speed : 300;
          speed = speed < 1500 ? speed : 1500;
          parent.find("ul:not(.children)").animate({ height: H }, speed);
          return false;
        });
    },
    rating: function () {
      var radio = $('.comments-rating input[type="radio"]');
      radio
        .on("click", function () {
          var el = $(this);
          var id = el.attr("id");
          $(".comments-rating .fn_radio").removeClass("clicked");
          $(".comments-rating ." + id).addClass("clicked");
        })
        .on("mouseenter", function () {
          var el = $(this);
          var id = el.attr("id");
          $(".comments-rating .fn_radio").removeClass("hovered");
          $(".comments-rating ." + id).addClass("hovered");
        })
        .on("mouseleave", function () {
          $(".comments-rating .fn_radio").removeClass("hovered");
        });
    },
    toTopJumper: function () {
      var totop = $(
        ".cavani_fn_footer .footer_totop a,a.cavani_fn_totop,.cavani_fn_footer .footer_right_totop a"
      );
      if (totop.length) {
        totop.on("click", function (e) {
          e.preventDefault();
          $("html, body").animate({ scrollTop: 0 }, "slow");
          return false;
        });
      }
    },
    widget__pages: function () {
      var nav = $(".widget_pages ul");
      nav.each(function () {
        $(this)
          .find("a")
          .off()
          .on("click", function (e) {
            var element = $(this);
            var parentItem = element.parent("li");
            var parentItems = element.parents("li");
            var parentUls = parentItem.parents("ul.children");
            var subMenu = element.next();
            var allSubMenusParents = nav.find("li");
            allSubMenusParents.removeClass("opened");
            if (subMenu.length) {
              e.preventDefault();
              if (!subMenu.parent("li").hasClass("active")) {
                if (!parentItems.hasClass("opened")) {
                  parentItems.addClass("opened");
                }
                allSubMenusParents.each(function () {
                  var el = $(this);
                  if (!el.hasClass("opened")) {
                    el.find("ul.children").slideUp();
                  }
                });
                allSubMenusParents.removeClass("active");
                parentUls.parent("li").addClass("active");
                subMenu.parent("li").addClass("active");
                subMenu.slideDown();
              } else {
                subMenu.parent("li").removeClass("active");
                subMenu.slideUp();
              }
              return false;
            }
          });
      });
    },
    submenu__Mobile: function () {
      var nav = $("ul.vert_menu_list, .widget_nav_menu ul.menu");
      var mobileAutoCollapse = CavaniWrapper.data("mobile-autocollapse");
      nav.each(function () {
        $(this)
          .find("a")
          .off()
          .on("click", function (e) {
            var element = $(this);
            var parentItem = element.parent("li");
            var parentItems = element.parents("li");
            var parentUls = parentItem.parents("ul.sub-menu");
            var subMenu = element.next();
            var allSubMenusParents = nav.find("li");
            allSubMenusParents.removeClass("opened");
            if (subMenu.length) {
              e.preventDefault();
              if (!subMenu.parent("li").hasClass("active")) {
                if (!parentItems.hasClass("opened")) {
                  parentItems.addClass("opened");
                }
                allSubMenusParents.each(function () {
                  var el = $(this);
                  if (!el.hasClass("opened")) {
                    el.find("ul.sub-menu").slideUp();
                  }
                });
                allSubMenusParents.removeClass("active");
                parentUls.parent("li").addClass("active");
                subMenu.parent("li").addClass("active");
                subMenu.slideDown();
              } else {
                subMenu.parent("li").removeClass("active");
                subMenu.slideUp();
              }
              return false;
            }
            if (mobileAutoCollapse === "enable") {
              if (nav.parent().parent().hasClass("opened")) {
                nav.parent().parent().removeClass("opened").slideUp();
                $(".cavani_fn_mobilemenu_wrap .hamburger").removeClass(
                  "is-active"
                );
              }
            }
          });
      });
    },
    hamburgerOpener__Mobile: function () {
      var hamburger = $(".cavani_fn_mobilemenu_wrap .hamburger");
      hamburger.off().on("click", function () {
        var element = $(this);
        var menupart = $(".cavani_fn_mobilemenu_wrap .mobilemenu");
        if (element.hasClass("is-active")) {
          element.removeClass("is-active");
          menupart.removeClass("opened");
          menupart.slideUp(500);
        } else {
          element.addClass("is-active");
          menupart.addClass("opened");
          menupart.slideDown(500);
        }
        return false;
      });
    },
    imgToSVG: function () {
      $("img.cavani_fn_svg,img.cavani_w_fn_svg").each(function () {
        var img = $(this);
        var imgClass = img.attr("class");
        var imgURL = img.attr("src");
        $.get(
          imgURL,
          function (data) {
            var svg = $(data).find("svg");
            if (typeof imgClass !== "undefined") {
              svg = svg.attr("class", imgClass + " replaced-svg");
            }
            img.replaceWith(svg);
          },
          "xml"
        );
      });
    },
    dataFnStyle: function () {
      $("[data-fn-style]").each(function () {
        var el = $(this);
        var s = el.attr("data-fn-style");
        $.each(s.split(";"), function (i, e) {
          el.css(e.split(":")[0], e.split(":")[1]);
        });
      });
    },
    dataFnBgImg: function () {
      var bgImage = $("*[data-fn-bg-img]");
      bgImage.each(function () {
        var element = $(this);
        var attrBg = element.attr("data-fn-bg-img");
        var bgImg = element.data("fn-bg-img");
        if (typeof attrBg !== "undefined") {
          element.addClass("marketify-ready");
          if (bgImg === "") {
            return;
          }
          element.css({ backgroundImage: "url(" + bgImg + ")" });
        }
      });
      var bgImage2 = $("*[data-bg-img]");
      bgImage2.each(function () {
        var element = $(this);
        var attrBg = element.attr("data-bg-img");
        var bgImg = element.data("bg-img");
        if (typeof attrBg !== "undefined") {
          element.addClass("marketify-ready");
          if (bgImg === "") {
            return;
          }
          element.css({ backgroundImage: "url(" + bgImg + ")" });
        }
      });
    },
    isotopeMasonry: function () {
      var masonry = $(".cavani_fn_masonry");
      if ($().isotope) {
        masonry.each(function () {
          $(this).isotope({
            itemSelector: ".cavani_fn_masonry_in",
            masonry: {},
          });
        });
      }
    },
    estimateWidgetHeight: function () {
      var est = $(".cavani_fn_widget_estimate");
      est.each(function () {
        var el = $(this);
        var h1 = el.find(".helper1");
        var h2 = el.find(".helper2");
        var h3 = el.find(".helper3");
        var h4 = el.find(".helper4");
        var h5 = el.find(".helper5");
        var h6 = el.find(".helper6");
        var eW = el.outerWidth();
        var w1 = Math.floor((eW * 80) / 300);
        var w2 = eW - w1;
        var e1 = Math.floor((w1 * 55) / 80);
        h1.css({ borderLeftWidth: w1 + "px", borderTopWidth: e1 + "px" });
        h2.css({ borderRightWidth: w2 + "px", borderTopWidth: e1 + "px" });
        h3.css({ borderLeftWidth: w1 + "px", borderTopWidth: w1 + "px" });
        h4.css({ borderRightWidth: w2 + "px", borderTopWidth: w1 + "px" });
        h5.css({ borderLeftWidth: w1 + "px", borderTopWidth: w1 + "px" });
        h6.css({ borderRightWidth: w2 + "px", borderTopWidth: w1 + "px" });
      });
    },
  };
  $(document).ready(function () {
    CavaniInit.init();
  });
  $(window).on("resize", function (e) {
    e.preventDefault();
    setTimeout(function () {
      CavaniInit.minHeightForPages();
      CavaniInit.menuHover();
      CavaniInit.filterHover();
    }, 500);
    CavaniInit.isotopeMasonry();
    CavaniInit.projectCategoryFitler();
    CavaniInit.estimateWidgetHeight();
  });
  $(window).on("scroll", function (e) {
    e.preventDefault();
    CavaniInit.fixedTotopScroll();
  });
  $(window).on("load", function (e) {
    e.preventDefault();
    CavaniInit.isotopeMasonry();
    CavaniInit.projectCategoryFitler();
    setTimeout(function () {
      CavaniInit.menuHover();
      CavaniInit.projectCategoryFitler();
      CavaniInit.projectCategoryFitler();
    }, 100);
  });
  $(window).load("body", function () {
    CavaniInit.runPreloader();
  });
})(jQuery);
