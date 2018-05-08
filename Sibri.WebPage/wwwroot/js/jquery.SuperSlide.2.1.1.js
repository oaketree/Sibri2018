!
function(e) {
	e.fn.slide = function(a) {
		return e.fn.slide.defaults = {
			type: "slide",
			effect: "fade",
			autoPlay: !1,
			delayTime: 500,
			interTime: 2500,
			triggerTime: 150,
			defaultIndex: 0,
			titCell: ".hd li",
			mainCell: ".bd",
			targetCell: null,
			trigger: "mouseover",
			scroll: 1,
			vis: 1,
			titOnClassName: "on",
			autoPage: !1,
			prevCell: ".prev",
			nextCell: ".next",
			pageStateCell: ".pageState",
			opp: !1,
			pnLoop: !0,
			easing: "swing",
			startFun: null,
			endFun: null,
			switchLoad: null,
			playStateCell: ".playState",
			mouseOverStop: !0,
			defaultPlay: !0,
			returnDefault: !1
		}, this.each(function() {
			var b = e.extend({}, e.fn.slide.defaults, a),
				c = e(this),
				d = b.effect,
				g = e(b.prevCell, c),
				n = e(b.nextCell, c),
				ha = e(b.pageStateCell, c),
				J = e(b.playStateCell, c),
				q = e(b.titCell, c),
				k = q.size(),
				h = e(b.mainCell, c),
				l = h.children().size(),
				B = b.switchLoad,
				F = e(b.targetCell, c),
				f = parseInt(b.defaultIndex),
				r = parseInt(b.delayTime),
				T = parseInt(b.interTime);
			parseInt(b.triggerTime);
			var y, m = parseInt(b.scroll),
				w = parseInt(b.vis),
				M = "false" == b.autoPlay || 0 == b.autoPlay ? !1 : !0,
				U = "false" == b.opp || 0 == b.opp ? !1 : !0,
				t = "false" == b.autoPage || 0 == b.autoPage ? !1 : !0,
				N = "false" == b.pnLoop || 0 == b.pnLoop ? !1 : !0,
				V = "false" == b.mouseOverStop || 0 == b.mouseOverStop ? !1 : !0,
				K = "false" == b.defaultPlay || 0 == b.defaultPlay ? !1 : !0,
				H = "false" == b.returnDefault || 0 == b.returnDefault ? !1 : !0,
				v = 0,
				u = 0,
				L = 0,
				O = 0,
				z = b.easing,
				G = null,
				P = null,
				Q = null,
				C = b.titOnClassName,
				x = q.index(c.find("." + C)),
				W = f = -1 == x ? f : x,
				ca = f,
				I = f,
				p = l >= w ? 0 != l % m ? l % m : m : 0,
				D = "leftMarquee" == d || "topMarquee" == d ? !0 : !1,
				da = function() {
					e.isFunction(b.startFun) && b.startFun(f, k, c, e(b.titCell, c), h, F, g, n)
				},
				A = function() {
					e.isFunction(b.endFun) && b.endFun(f, k, c, e(b.titCell, c), h, F, g, n)
				},
				X = function() {
					q.removeClass(C);
					K && q.eq(ca).addClass(C)
				};
			if ("menu" == b.type) return K && q.removeClass(C).eq(f).addClass(C), q.hover(function() {
				y = e(this).find(b.targetCell);
				var a = q.index(e(this));
				P = setTimeout(function() {
					switch (f = a, q.removeClass(C).eq(f).addClass(C), da(), d) {
					case "fade":
						y.stop(!0, !0).animate({
							opacity: "show"
						}, r, z, A);
						break;
					case "slideDown":
						y.stop(!0, !0).animate({
							height: "show"
						}, r, z, A)
					}
				}, b.triggerTime)
			}, function() {
				switch (clearTimeout(P), d) {
				case "fade":
					y.animate({
						opacity: "hide"
					}, r, z);
					break;
				case "slideDown":
					y.animate({
						height: "hide"
					}, r, z)
				}
			}), H && c.hover(function() {
				clearTimeout(Q)
			}, function() {
				Q = setTimeout(X, r)
			}), void 0;
			if (0 == k && (k = l), D && (k = 2), t) {
				l >= w ? "leftLoop" == d || "topLoop" == d ? k = 0 != l % m ? (0 ^ l / m) + 1 : l / m : (t = l - w, k = 1 + parseInt(0 != t % m ? t / m + 1 : t / m), 0 >= k && (k = 1)) : k = 1;
				q.html("");
				t = "";
				if (1 == b.autoPage || "true" == b.autoPage) for (x = 0; k > x; x++) t += "<li>" + (x + 1) + "</li>";
				else for (x = 0; k > x; x++) t += b.autoPage.replace("$", x + 1);
				q.html(t);
				q = q.children()
			}
			if (l >= w) {
				h.children().each(function() {
					e(this).width() > L && (L = e(this).width(), u = e(this).outerWidth(!0));
					e(this).height() > O && (O = e(this).height(), v = e(this).outerHeight(!0))
				});
				var ea = h.children(),
					t = function() {
						for (var a = 0; w > a; a++) ea.eq(a).clone().addClass("clone").appendTo(h);
						for (a = 0; p > a; a++) ea.eq(l - a - 1).clone().addClass("clone").prependTo(h)
					};
				switch (d) {
				case "fold":
					h.css({
						position: "relative",
						width: u,
						height: v
					}).children().css({
						position: "absolute",
						width: L,
						left: 0,
						top: 0,
						display: "none"
					});
					break;
				case "top":
					h.wrap('<div class="tempWrap" style="overflow:hidden; position:relative; height:' + w * v + 'px"></div>').css({
						top: -(f * m) * v,
						position: "relative",
						padding: "0",
						margin: "0"
					}).children().css({
						height: O
					});
					break;
				case "left":
					h.wrap('<div class="tempWrap" style="overflow:hidden; position:relative; width:' + w * u + 'px"></div>').css({
						width: l * u,
						left: -(f * m) * u,
						position: "relative",
						overflow: "hidden",
						padding: "0",
						margin: "0"
					}).children().css({
						"float": "left",
						width: L
					});
					break;
				case "leftLoop":
				case "leftMarquee":
					t();
					h.wrap('<div class="tempWrap" style="overflow:hidden; position:relative; width:' + w * u + 'px"></div>').css({
						width: (l + w + p) * u,
						position: "relative",
						overflow: "hidden",
						padding: "0",
						margin: "0",
						left: -(p + f * m) * u
					}).children().css({
						"float": "left",
						width: L
					});
					break;
				case "topLoop":
				case "topMarquee":
					t(), h.wrap('<div class="tempWrap" style="overflow:hidden; position:relative; height:' + w * v + 'px"></div>').css({
						height: (l + w + p) * v,
						position: "relative",
						padding: "0",
						margin: "0",
						top: -(p + f * m) * v
					}).children().css({
						height: O
					})
				}
			}
			var Y = function(a) {
					var b = a * m;
					return a == k ? b = l : -1 == a && 0 != l % m && (b = -l % m), b
				},
				fa = function(a) {
					var b = function(b) {
							for (var c = b; w + b > c; c++) a.eq(c).find("img[" + B + "]").each(function() {
								var a = e(this);
								if (a.attr("src", a.attr(B)).removeAttr(B), h.find(".clone")[0]) for (var b = h.children(), c = 0; c < b.size(); c++) b.eq(c).find("img[" + B + "]").each(function() {
									e(this).attr(B) == a.attr("src") && e(this).attr("src", e(this).attr(B)).removeAttr(B)
								})
							})
						};
					switch (d) {
					case "fade":
					case "fold":
					case "top":
					case "left":
					case "slideDown":
						b(f * m);
						break;
					case "leftLoop":
					case "topLoop":
						b(p + Y(I));
						break;
					case "leftMarquee":
					case "topMarquee":
						var c = "leftMarquee" == d ? h.css("left").replace("px", "") : h.css("top").replace("px", ""),
							g = "leftMarquee" == d ? u : v,
							k = p;
						0 != c % g && (c = Math.abs(0 ^ c / g), k = 1 == f ? p + c : p + c - 1);
						b(k)
					}
				},
				E = function(a) {
					if (!K || W != f || a || D) {
						if (D ? 1 <= f ? f = 1 : 0 >= f && (f = 0) : (I = f, f >= k ? f = 0 : 0 > f && (f = k - 1)), da(), null != B && fa(h.children()), F[0] && (y = F.eq(f), null != B && fa(F), "slideDown" == d ? (F.not(y).stop(!0, !0).slideUp(r), y.slideDown(r, z, function() {
							h[0] || A()
						})) : (F.not(y).stop(!0, !0).hide(), y.animate({
							opacity: "show"
						}, r, function() {
							h[0] || A()
						}))), l >= w) switch (d) {
						case "fade":
							h.children().stop(!0, !0).eq(f).animate({
								opacity: "show"
							}, r, z, function() {
								A()
							}).siblings().hide();
							break;
						case "fold":
							h.children().stop(!0, !0).eq(f).animate({
								opacity: "show"
							}, r, z, function() {
								A()
							}).siblings().animate({
								opacity: "hide"
							}, r, z);
							break;
						case "top":
							h.stop(!0, !1).animate({
								top: -f * m * v
							}, r, z, function() {
								A()
							});
							break;
						case "left":
							h.stop(!0, !1).animate({
								left: -f * m * u
							}, r, z, function() {
								A()
							});
							break;
						case "leftLoop":
							var b = I;
							h.stop(!0, !0).animate({
								left: -(Y(I) + p) * u
							}, r, z, function() {
								-1 >= b ? h.css("left", -(p + (k - 1) * m) * u) : b >= k && h.css("left", -p * u);
								A()
							});
							break;
						case "topLoop":
							b = I;
							h.stop(!0, !0).animate({
								top: -(Y(I) + p) * v
							}, r, z, function() {
								-1 >= b ? h.css("top", -(p + (k - 1) * m) * v) : b >= k && h.css("top", -p * v);
								A()
							});
							break;
						case "leftMarquee":
							a = h.css("left").replace("px", "");
							0 == f ? h.animate({
								left: ++a
							}, 0, function() {
								0 <= h.css("left").replace("px", "") && h.css("left", -l * u)
							}) : h.animate({
								left: --a
							}, 0, function() {
								h.css("left").replace("px", "") <= -(l + p) * u && h.css("left", -p * u)
							});
							break;
						case "topMarquee":
							a = h.css("top").replace("px", ""), 0 == f ? h.animate({
								top: ++a
							}, 0, function() {
								0 <= h.css("top").replace("px", "") && h.css("top", -l * v)
							}) : h.animate({
								top: --a
							}, 0, function() {
								h.css("top").replace("px", "") <= -(l + p) * v && h.css("top", -p * v)
							})
						}
						q.removeClass(C).eq(f).addClass(C);
						W = f;
						N || (n.removeClass("nextStop"), g.removeClass("prevStop"), 0 == f && g.addClass("prevStop"), f == k - 1 && n.addClass("nextStop"));
						ha.html("<span>" + (f + 1) + "</span>/" + k)
					}
				};
			K && E(!0);
			H && c.hover(function() {
				clearTimeout(Q)
			}, function() {
				Q = setTimeout(function() {
					f = ca;
					K ? E() : "slideDown" == d ? y.slideUp(r, X) : y.animate({
						opacity: "hide"
					}, r, X);
					W = f
				}, 300)
			});
			var Z = function(a) {
					G = setInterval(function() {
						U ? f-- : f++;
						E()
					}, a ? a : T)
				},
				R = function(a) {
					G = setInterval(E, a ? a : T)
				},
				S = function() {
					V || (clearInterval(G), Z())
				},
				H = function() {
					(N || f != k - 1) && (f++, E(), D || S())
				},
				t = function() {
					(N || 0 != f) && (f--, E(), D || S())
				},
				aa = function() {
					clearInterval(G);
					D ? R() : Z();
					J.removeClass("pauseState")
				},
				ba = function() {
					clearInterval(G);
					J.addClass("pauseState")
				};
			if (M ? D ? (U ? f-- : f++, R(), V && h.hover(ba, aa)) : (Z(), V && c.hover(ba, aa)) : (D && (U ? f-- : f++), J.addClass("pauseState")), J.click(function() {
				J.hasClass("pauseState") ? aa() : ba()
			}), "mouseover" == b.trigger ? q.hover(function() {
				var a = q.index(this);
				P = setTimeout(function() {
					f = a;
					E();
					S()
				}, b.triggerTime)
			}, function() {
				clearTimeout(P)
			}) : q.click(function() {
				f = q.index(this);
				E();
				S()
			}), D) {
				if (n.mousedown(H), g.mousedown(t), N) {
					var ga, M = function() {
							ga = setTimeout(function() {
								clearInterval(G);
								R(0 ^ T / 10)
							}, 150)
						},
						x = function() {
							clearTimeout(ga);
							clearInterval(G);
							R()
						};
					n.mousedown(M);
					n.mouseup(x);
					g.mousedown(M);
					g.mouseup(x)
				}
				"mouseover" == b.trigger && (n.hover(H, function() {}), g.hover(t, function() {}))
			} else n.click(H), g.click(t)
		})
	}
}(jQuery);
jQuery.easing.jswing = jQuery.easing.swing;
jQuery.extend(jQuery.easing, {
	def: "easeOutQuad",
	swing: function(e, a, b, c, d) {
		return jQuery.easing[jQuery.easing.def](e, a, b, c, d)
	},
	easeInQuad: function(e, a, b, c, d) {
		return c * (a /= d) * a + b
	},
	easeOutQuad: function(e, a, b, c, d) {
		return -c * (a /= d) * (a - 2) + b
	},
	easeInOutQuad: function(e, a, b, c, d) {
		return 1 > (a /= d / 2) ? c / 2 * a * a + b : -c / 2 * (--a * (a - 2) - 1) + b
	},
	easeInCubic: function(e, a, b, c, d) {
		return c * (a /= d) * a * a + b
	},
	easeOutCubic: function(e, a, b, c, d) {
		return c * ((a = a / d - 1) * a * a + 1) + b
	},
	easeInOutCubic: function(e, a, b, c, d) {
		return 1 > (a /= d / 2) ? c / 2 * a * a * a + b : c / 2 * ((a -= 2) * a * a + 2) + b
	},
	easeInQuart: function(e, a, b, c, d) {
		return c * (a /= d) * a * a * a + b
	},
	easeOutQuart: function(e, a, b, c, d) {
		return -c * ((a = a / d - 1) * a * a * a - 1) + b
	},
	easeInOutQuart: function(e, a, b, c, d) {
		return 1 > (a /= d / 2) ? c / 2 * a * a * a * a + b : -c / 2 * ((a -= 2) * a * a * a - 2) + b
	},
	easeInQuint: function(e, a, b, c, d) {
		return c * (a /= d) * a * a * a * a + b
	},
	easeOutQuint: function(e, a, b, c, d) {
		return c * ((a = a / d - 1) * a * a * a * a + 1) + b
	},
	easeInOutQuint: function(e, a, b, c, d) {
		return 1 > (a /= d / 2) ? c / 2 * a * a * a * a * a + b : c / 2 * ((a -= 2) * a * a * a * a + 2) + b
	},
	easeInSine: function(e, a, b, c, d) {
		return -c * Math.cos(a / d * (Math.PI / 2)) + c + b
	},
	easeOutSine: function(e, a, b, c, d) {
		return c * Math.sin(a / d * (Math.PI / 2)) + b
	},
	easeInOutSine: function(e, a, b, c, d) {
		return -c / 2 * (Math.cos(Math.PI * a / d) - 1) + b
	},
	easeInExpo: function(e, a, b, c, d) {
		return 0 == a ? b : c * Math.pow(2, 10 * (a / d - 1)) + b
	},
	easeOutExpo: function(e, a, b, c, d) {
		return a == d ? b + c : c * (-Math.pow(2, -10 * a / d) + 1) + b
	},
	easeInOutExpo: function(e, a, b, c, d) {
		return 0 == a ? b : a == d ? b + c : 1 > (a /= d / 2) ? c / 2 * Math.pow(2, 10 * (a - 1)) + b : c / 2 * (-Math.pow(2, -10 * --a) + 2) + b
	},
	easeInCirc: function(e, a, b, c, d) {
		return -c * (Math.sqrt(1 - (a /= d) * a) - 1) + b
	},
	easeOutCirc: function(e, a, b, c, d) {
		return c * Math.sqrt(1 - (a = a / d - 1) * a) + b
	},
	easeInOutCirc: function(e, a, b, c, d) {
		return 1 > (a /= d / 2) ? -c / 2 * (Math.sqrt(1 - a * a) - 1) + b : c / 2 * (Math.sqrt(1 - (a -= 2) * a) + 1) + b
	},
	easeInElastic: function(e, a, b, c, d) {
		e = 0;
		var g = c;
		if (0 == a) return b;
		if (1 == (a /= d)) return b + c;
		(e || (e = .3 * d), g < Math.abs(c)) ? (g = c, c = e / 4) : c = e / (2 * Math.PI) * Math.asin(c / g);
		return -(g * Math.pow(2, 10 * --a) * Math.sin(2 * (a * d - c) * Math.PI / e)) + b
	},
	easeOutElastic: function(e, a, b, c, d) {
		var g = 0,
			n = c;
		if (0 == a) return b;
		if (1 == (a /= d)) return b + c;
		(g || (g = .3 * d), n < Math.abs(c)) ? (n = c, e = g / 4) : e = g / (2 * Math.PI) * Math.asin(c / n);
		return n * Math.pow(2, -10 * a) * Math.sin(2 * (a * d - e) * Math.PI / g) + c + b
	},
	easeInOutElastic: function(e, a, b, c, d) {
		var g = 0,
			n = c;
		if (0 == a) return b;
		if (2 == (a /= d / 2)) return b + c;
		(g || (g = .3 * d * 1.5), n < Math.abs(c)) ? (n = c, e = g / 4) : e = g / (2 * Math.PI) * Math.asin(c / n);
		return 1 > a ? -.5 * n * Math.pow(2, 10 * --a) * Math.sin(2 * (a * d - e) * Math.PI / g) + b : .5 * n * Math.pow(2, -10 * --a) * Math.sin(2 * (a * d - e) * Math.PI / g) + c + b
	},
	easeInBack: function(e, a, b, c, d, g) {
		return void 0 == g && (g = 1.70158), c * (a /= d) * a * ((g + 1) * a - g) + b
	},
	easeOutBack: function(e, a, b, c, d, g) {
		return void 0 == g && (g = 1.70158), c * ((a = a / d - 1) * a * ((g + 1) * a + g) + 1) + b
	},
	easeInOutBack: function(e, a, b, c, d, g) {
		return void 0 == g && (g = 1.70158), 1 > (a /= d / 2) ? c / 2 * a * a * (((g *= 1.525) + 1) * a - g) + b : c / 2 * ((a -= 2) * a * (((g *= 1.525) + 1) * a + g) + 2) + b
	},
	easeInBounce: function(e, a, b, c, d) {
		return c - jQuery.easing.easeOutBounce(e, d - a, 0, c, d) + b
	},
	easeOutBounce: function(e, a, b, c, d) {
		return (a /= d) < 1 / 2.75 ? 7.5625 * c * a * a + b : 2 / 2.75 > a ? c * (7.5625 * (a -= 1.5 / 2.75) * a + .75) + b : 2.5 / 2.75 > a ? c * (7.5625 * (a -= 2.25 / 2.75) * a + .9375) + b : c * (7.5625 * (a -= 2.625 / 2.75) * a + .984375) + b
	},
	easeInOutBounce: function(e, a, b, c, d) {
		return d / 2 > a ? .5 * jQuery.easing.easeInBounce(e, 2 * a, 0, c, d) + b : .5 * jQuery.easing.easeOutBounce(e, 2 * a - d, 0, c, d) + .5 * c + b
	}
});