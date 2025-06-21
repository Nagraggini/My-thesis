/*!CK:2911134548!*//*1438092812,*/

if (self.CavalryLogger) { CavalryLogger.start_js(["p\/8On"]); }

__d("PagePluginActionLogger",["BanzaiLogger","DOM","Event","PluginActions","PluginActionTypes"],function(a,b,c,d,e,f,g,h,i,j,k){b.__markCompiled&&b.__markCompiled();var l={initializeClickLoggers:function(m,n,o,p,q,r,s,t){var u=function(v,w){try{var y=h.find(n,'.'+v);i.listen(y,'click',function(z){g.log('PagePluginActionsLoggerConfig',{page_id:m,page_plugin_action:w,page_plugin_action_type:k.CLICK});});}catch(x){}};u(o,j.PAGE_LIKE);u(p,j.PAGE_UNLIKE);u(q,j.PAGE_AVATAR);u(r,j.PAGE_PERMALINK);u(s,j.PAGE_SHARE);u(t,j.PAGE_CTA);}};e.exports=l;},null);