using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using foodAPI.Models;

namespace foodAPI.Areas.User.Controllers
{
    /// <summary>
    /// FOOD_orderController City Food
     /// </summary>
    public class FOOD_orderController : BaseController
    {
        /// <summary>
        /// 資料列表
        /// </summary>
        /// <param name="page">目前頁數</param>
        /// <param name="searchText">搜尋文字</param>
        /// <returns></returns>
        [HttpGet]
        [JwtAuthActionFilter()]
        public ActionResult Index(int page = 1, string searchText = "")
        {
            //檢查瀏覽權限
            if (!PrgService.IsProgramSecurity(enSecurtyMode.Index))
                return RedirectToAction(ActionService.Index, ActionService.Home, new { area = ActionService.Area });

            using (z_repoorder repos = new z_repoorder())
            {
                var modelData = repos.GetDapperDataList(searchText);
                var model = modelData.ToPagedList(page, PrgService.PageSize);
                ViewBag.SearchText = searchText;
                ViewBag.PageInfo = PrgService.SetIndex(model.PageNumber, model.PageCount, searchText);
                return View(model);
            }
        }

        /// <summary>
        /// 明細
        /// </summary>
        /// <param name="id">記錄 ID</param>
        /// <returns></returns>
        [HttpGet]
        [JwtAuthActionFilter()]
        public ActionResult Detail(string id)
        {
            using (z_repoorder repos = new z_repoorder())
            {
                PrgService.SetAction(enAction.Detail, enCardSize.Medium);
                var model = repos.repo.ReadSingle(m => m.orderId == id);
                return View(model);
            }
        }

        /// <summary>
        /// 新增/修改
        /// </summary>
        /// <param name="id">記錄 ID</param>
        /// <returns></returns>
        [HttpGet]
        [JwtAuthActionFilter()]
        public ActionResult CreateEdit(string id)
        {
            
            using (z_repoorder repos = new z_repoorder())
            {
				        var model = repos.repo.ReadSingle(m => m.orderId == id);
				        if (model == null)
                {
                        model = new order();
//                    // 設定新增預設值
//                    using (AttributeService attr = new AttributeService())
//                    {
//                        model.orderId = (string)attr.GetDefaultValue<z_metaorder>("orderId");
//                        model.userId = (int)attr.GetDefaultValue<z_metaorder>("userId");
//                        model.userName = (string)attr.GetDefaultValue<z_metaorder>("userName");
//                        model.totalPrice = (int)attr.GetDefaultValue<z_metaorder>("totalPrice");
//                        model.dateTime = (DateTime)attr.GetDefaultValue<z_metaorder>("dateTime");
//                        model.takeAway = (string)attr.GetDefaultValue<z_metaorder>("takeAway");
//                        model.isDone = (string)attr.GetDefaultValue<z_metaorder>("isDone");
//                        model.remark = (string)attr.GetDefaultValue<z_metaorder>("remark");
//                    }
                }
                return View(model);
            }
        }

        /// <summary>
        /// 新增/修改
        /// </summary>
        /// <param name="model">資料</param>
        /// <returns></returns>
        [HttpPost]
        [JwtAuthActionFilter()]
        public ActionResult CreateEdit(order model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = ActionService.SetErrorMessage<z_metaorder>(ModelState);
                return View(model);
            }
            using (z_repoorder repos = new z_repoorder())
            {
                repos.CreateEdit(model);
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
            }
       }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="id">記錄 ID</param>
       /// <returns></returns>
        [HttpPost]
        [JwtAuthActionFilter()]
        public ActionResult Delete(string id )
        {
            //檢查刪除權限
            if (!PrgService.IsProgramSecurity(enSecurtyMode.Delete))
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });

            using (z_repoorder repos = new z_repoorder())
            {
                repos.Delete(id);
                dmJsonMessage result = new dmJsonMessage() { Mode = true, Message = "資料已刪除!!" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 選取
        /// </summary>
        /// <param name="id">記錄 ID</param>
        /// <returns></returns>
        [HttpGet]
        [JwtAuthActionFilter()]
        public ActionResult Select(int id = 0)
        {
            PrgService.SelectedId = id;
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area, page = PrgService.PageNumber, searchText = PrgService.SearchText });
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [JwtAuthActionFilter()]
        public ActionResult Search()
        {
            object obj_text = Request.Form[ActionService.SearchText];
            string str_text = (obj_text == null) ? string.Empty : obj_text.ToString();
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area, searchText = str_text });
        }
    }
}
