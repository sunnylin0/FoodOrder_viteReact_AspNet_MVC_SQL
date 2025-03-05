
DROP TABLE IF EXISTS [category]
DROP TABLE IF EXISTS [catTOsub]
DROP TABLE IF EXISTS [detail]
DROP TABLE IF EXISTS [detailSubjoin]
DROP TABLE IF EXISTS [menu]
DROP TABLE IF EXISTS [menuSubjoinList]
DROP TABLE IF EXISTS [order]
DROP TABLE IF EXISTS [subCategory]
DROP TABLE IF EXISTS [subjoin]
DROP TABLE IF EXISTS [users]



CREATE TABLE [category] (catId NVARCHAR(255), catName NVARCHAR(255), CONSTRAINT PK_category PRIMARY KEY (catId));

CREATE TABLE [catTOsub] (cad2subId INT IDENTITY(1,1) NOT NULL, cadId NVARCHAR(255), subCatId INT, CONSTRAINT PK_catTOsub PRIMARY KEY (cad2subId));

CREATE TABLE [detail] (detailId INT IDENTITY(1,1) NOT NULL, orderId NVARCHAR(255), menuId NVARCHAR(255), menuName NVARCHAR(255), price INT, subPrice INT, qty INT, remark NVARCHAR(255), CONSTRAINT PK_detail PRIMARY KEY (detailId));

CREATE TABLE [detailSubjoin] (dsId INT IDENTITY(1,1) NOT NULL, detailId INT, subId INT, CONSTRAINT PK_detailSubjoin PRIMARY KEY (dsId));

CREATE TABLE [menu] (menuId NVARCHAR(255), catId NVARCHAR(255), menuNameEn NVARCHAR(255), menuName NVARCHAR(255), comment NVARCHAR(255), price INT, img NVARCHAR(255), isSoldOut bit, CONSTRAINT PK_menu PRIMARY KEY (menuId));

CREATE TABLE [menuSubjoinList] (menuSubListId INT IDENTITY(1,1) NOT NULL, menuId NVARCHAR(255), subCatId NVARCHAR(255), CONSTRAINT PK_menuSubjoinList PRIMARY KEY (menuSubListId));

CREATE TABLE [order] (orderId NVARCHAR(255), userId INT, userName NVARCHAR(255), totalPrice INT, dateTime DATETIME, takeAway bit, isDone bit, remark NVARCHAR(MAX), CONSTRAINT PK_order PRIMARY KEY (orderId));


CREATE TABLE [subCategory] (subCatId NVARCHAR(255), subCatName NVARCHAR(255), isMulti bit, CONSTRAINT PK_subCategory PRIMARY KEY (subCatId));

CREATE TABLE [subjoin] (subId INT IDENTITY(1,1) NOT NULL, subCatId NVARCHAR(255), subName NVARCHAR(255), subPrice INT, CONSTRAINT PK_subjoin PRIMARY KEY (subId));

CREATE TABLE [users] (userId INT IDENTITY(1,1) NOT NULL, userName NVARCHAR(255), password NVARCHAR(255), phone NVARCHAR(255), email NVARCHAR(255), role NVARCHAR(255), CONSTRAINT PK_users PRIMARY KEY (userId));


INSERT INTO [category] (catId, catName) VALUES (N'c01', N'前菜');
INSERT INTO [category] (catId, catName) VALUES (N'c02', N'沙拉');
INSERT INTO [category] (catId, catName) VALUES (N'c03', N'湯品');
INSERT INTO [category] (catId, catName) VALUES (N'c04', N'主菜');
INSERT INTO [category] (catId, catName) VALUES (N'c05', N'點心');
INSERT INTO [category] (catId, catName) VALUES (N'c06', N'飲品');

SET IDENTITY_INSERT [dbo].[detail] ON 
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (1, N'OD1669619419597', N'p051', N'歡樂薯餅', 10, 0, 4, N'請幫我加很多番茄醬');
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (2, N'OD1669619419597', N'p061', N'早餐店奶茶', 15, 0, 1, NULL);
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (3, N'OD1669619419597', N'p021', N'果醬吐司', 15, 0, 1, NULL);
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (4, N'OD1669622562629', N'p034', N'日式和牛堡', 100, NULL, 1, NULL);
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (5, N'OD1669622562629', N'p062', N'經典紅茶', 15, NULL, 1, NULL);
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (6, N'OD1670063897679', N'p061', N'早餐店奶茶', 15, 10, 1, NULL);
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (7, N'OD1670063897679', N'p012', N'玉米蛋餅', 50, 0, 2, NULL);
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (8, N'OD1670067346035', N'p051', N'歡樂薯餅', 10, 0, 4, NULL);
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (9, N'OD1670067346035', N'p042', N'低脂蛋白沙拉', 80, 0, 1, NULL);
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (10, N'OD1670067346035', N'p013', N'培根蛋餅', 40, 0, 2, NULL);
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (11, N'OD1670067346035', N'p062', N'經典紅茶', 15, 0, 2, NULL);
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (12, N'OD1670067346035', N'p061', N'早餐店奶茶', 15, 10, 1, NULL);
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (13, N'OD1670067346035', N'p061', N'早餐店奶茶', 15, 0, 1, NULL);
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (14, N'OD1670121729628', N'p034', N'日式和牛堡', 120, 0, 1, NULL);
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (15, N'OD1670121729628', N'p051', N'歡樂薯餅', 10, 0, 2, NULL);
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (16, N'OD1670121729628', N'p061', N'早餐店奶茶', 15, 0, 1, NULL);
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (17, N'OD1670122266392', N'p032', N'美味蟹堡', 55, 0, 1, NULL);
INSERT INTO [detail] (detailId, orderId, menuId, menuName, price, subPrice, qty, remark) VALUES (18, N'OD1670122266392', N'p062', N'經典紅茶', 15, 0, 1, NULL);

SET IDENTITY_INSERT [dbo].[detail] OFF
GO




SET IDENTITY_INSERT [dbo].[detailSubjoin] ON 
INSERT INTO [detailSubjoin] (dsId, detailId, subId) VALUES (1, 1, 2);
INSERT INTO [detailSubjoin] (dsId, detailId, subId) VALUES (2, 1, 16);
INSERT INTO [detailSubjoin] (dsId, detailId, subId) VALUES (3, 2, 6);
INSERT INTO [detailSubjoin] (dsId, detailId, subId) VALUES (4, 3, 12);
INSERT INTO [detailSubjoin] (dsId, detailId, subId) VALUES (5, 4, 1);
INSERT INTO [detailSubjoin] (dsId, detailId, subId) VALUES (6, 4, 2);
INSERT INTO [detailSubjoin] (dsId, detailId, subId) VALUES (7, 4, 10);
INSERT INTO [detailSubjoin] (dsId, detailId, subId) VALUES (8, 6, 5);
INSERT INTO [detailSubjoin] (dsId, detailId, subId) VALUES (9, 7, 12);
INSERT INTO [detailSubjoin] (dsId, detailId, subId) VALUES (10, 8, 13);
INSERT INTO [detailSubjoin] (dsId, detailId, subId) VALUES (11, 12, 5);
INSERT INTO [detailSubjoin] (dsId, detailId, subId) VALUES (12, 12, 9);
INSERT INTO [detailSubjoin] (dsId, detailId, subId) VALUES (13, 13, 5);
INSERT INTO [detailSubjoin] (dsId, detailId, subId) VALUES (14, 17, 10);
SET IDENTITY_INSERT [dbo].[detailSubjoin] OFF
GO



INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p012', N'c01', N'cheese_tar', N'布里起司塔佐蔓越莓醬', N'蔓越莓醬佐特製布里起司', 180, N'./Img/PC/aa1.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p013', N'c01', N'bacon_scallops', N'培根扇貝捲佐香醋蛋黃醬', N'高級培根+生食級干貝', 200, N'./Img/PC/aa9.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p014', N'c01', N'devil_egg', N'美味香辣惡魔蛋', N'放山雞蛋+特製芥末醬', 130, N'./Img/PC/aa4.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p015', N'c01', N'uni_truffle', N'炙燒黑松露海膽', N'極致鮮美的海陸首選', 200, N'./Img/PC/aa2.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p016', N'c01', N'cheese_ball', N'蜜餞山核桃蔓越莓山羊起司球', N'精選果乾堅果+爆漿山羊起司', 150, N'./Img/PC/aa5.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p021', N'c02', N'dawn_salad', N'晨曦之露沙拉', N'清新脆口的沙拉帶來早晨露珠般的清爽感覺', 130, N'./Img/PC/ss2.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p022', N'c02', N'green_salad', N'綠野仙?沙拉', N'彷彿讓人置身於森林之中感受自然的豐富風味', 150, N'./Img/PC/ss4.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p023', N'c02', N'sun_salad', N'陽光綻放沙拉', N'充滿了陽光般的明亮色彩與活力讓人感受到夏日的溫暖。', 130, N'./Img/PC/ss5.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p024', N'c02', N'rainbow_salad', N'彩虹果園沙拉', N'繽紛的水果搭配脆爽的生菜像彩虹般絢麗的視覺享受。', 150, N'./Img/PC/ss6.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p031', N'c03', N'ee1', N'紐澳良豬排堡', N'就是豬排加生菜的漢堡啦', 55, N'./Img/PC/p031.jpg', N'True');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p032', N'c03', N'ee2', N'美味蟹堡', N'是誰住在深海的大鳳梨裡', 45, N'./Img/PC/p032.jpg', N'True');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p033', N'c03', N'cheese_soup', N'綠花椰菜切達起士濃湯', N'濃鬱、濃鬱、起司的味道。', 120, N'./Img/PC/pp2.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p034', N'c03', N'bean_soup', N'糯米椒白豆湯', N'具有美味的奶油味和白豆的舒適感', 120, N'./Img/PC/pp3.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p035', N'c03', N'tortilla_soup', N'素食玉米餅湯', N'豐盛、溫暖、充滿大膽的味道。', 120, N'./Img/PC/pp4.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p036', N'c03', N'squash_soup', N'橡子南瓜湯', N'木質百里香、溫暖的肉荳蔻和辣椒調味味道鮮美', 120, N'./Img/PC/pp5.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p041', N'c04', N'red_snappe', N'紅鯛魚佐柑橘醬', N'融合了原始菜餚的所有明亮、酥脆的味道', 560, N'./Img/PC/mm11.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p042', N'c04', N'ny_steak', N'紐約客牛排佐紅酒醬', N'特製紅酒醬配精選紐約客', 650, N'./Img/PC/mm8.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p043', N'c04', N'lobster', N'帕爾瑪乾酪龍蝦', N'閃閃發光的龍蝦鮮甜味', 780, N'./Img/PC/mm12.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p044', N'c04', N'class_duck', N'經典油封鴨', N'每一口都會在嘴裡融化', 680, N'./Img/PC/mm13.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p045', N'c04', N'pancake_egg', N'蔬菜煎餅半熟蛋', N'蛋素草食餐', 300, N'./Img/PC/mm1.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p046', N'c04', N'special_brunch', N'主廚特製早午餐', N'滿足一天的所需', 460, N'./Img/PC/mm2.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p051', N'c05', N'berry_waffles', N'莓好焦糖鬆餅', N'酸甜莓果襯托出鬆餅的美好', 180, N'./Img/PC/dd2.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p052', N'c05', N'honey_waffles', N'經典蜂蜜鬆餅', N'簡單又不會尷尬的甜香', 150, N'./Img/PC/dd5.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p053', N'c05', N'Cranberry_puffs', N'蔓越莓一口酥', N'甜香酥脆一口一個', 160, N'./Img/PC/dd6.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p054', N'c05', N'grape_puff', N'晴王葡萄澎派', N'滿滿麝香葡萄太過癮', 220, N'./Img/PC/dd7.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p055', N'c05', N'Chestnut_cake', N'栗拔山兮蛋糕', N'栗子泥加上好幾顆栗子的美好', 200, N'./Img/PC/dd8.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p061', N'c06', N'coffee_latte', N'夢幻咖啡拿鐵', N'哥倫比單品深烘培日曬處理', 180, N'./Img/PC/latte3.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p062', N'c06', N'macha_latte', N'頂級抹茶拿鐵', N'宇治高級抹茶配上小農鮮乳', 180, N'./Img/PC/macha2.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p063', N'c06', N'cocktail_rum', N'熱帶蘭姆氣旋', N'加勒比熱帶風情', 180, N'./Img/PC/cock1.jpg', N'False');
INSERT INTO [menu] (menuId, catId, menuNameEn, menuName, comment, price, img, isSoldOut) VALUES (N'p064', N'c06', N'fruits_tea', N'綜合水果茶', N'新鮮酸甜好滋味', 180, N'./Img/PC/tea1.jpg', N'False');
GO


SET IDENTITY_INSERT [dbo].[menuSubjoinList] ON 
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (1, N'p012', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (2, N'p013', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (3, N'p014', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (4, N'p015', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (5, N'p016', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (6, N'p021', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (7, N'p022', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (8, N'p023', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (9, N'p024', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (10, N'p031', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (11, N'p032', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (12, N'p033', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (13, N'p034', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (14, N'p035', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (15, N'p036', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (16, N'p041', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (17, N'p042', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (18, N'p043', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (19, N'p044', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (20, N'p045', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (21, N'p046', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (22, N'p041', N'AH04');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (23, N'p042', N'AH04');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (24, N'p043', N'AH04');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (25, N'p044', N'AH04');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (26, N'p045', N'AH04');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (27, N'p046', N'AH04');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (28, N'p051', N'AH01');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (29, N'p051', N'AH04');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (30, N'p061', N'AH02');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (31, N'p062', N'AH02');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (32, N'p063', N'AH02');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (33, N'p064', N'AH02');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (34, N'p061', N'AH03');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (35, N'p062', N'AH03');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (36, N'p063', N'AH03');
INSERT INTO [menuSubjoinList] (menuSubListId, menuId, subCatId) VALUES (37, N'p064', N'AH03');
SET IDENTITY_INSERT [dbo].[menuSubjoinList] OFF
GO



INSERT INTO [order] (orderId, userId, userName, totalPrice, dateTime, takeAway, isDone, remark) VALUES (N'OD1669619419597', 2, N'Cake', 70, N'2022/11/28', N'True', N'False', NULL);
INSERT INTO [order] (orderId, userId, userName, totalPrice, dateTime, takeAway, isDone, remark) VALUES (N'OD1669622562629', 3, N'小明', 115, N'2022/11/28', N'True', N'True', NULL);
INSERT INTO [order] (orderId, userId, userName, totalPrice, dateTime, takeAway, isDone, remark) VALUES (N'OD1670063897679', 3, N'小明', 115, N'2022/12/3', N'True', N'False', N'謝謝老闆');
INSERT INTO [order] (orderId, userId, userName, totalPrice, dateTime, takeAway, isDone, remark) VALUES (N'OD1670067346035', 5, N'杰倫', 270, N'2022/12/3', N'True', N'True', NULL);
INSERT INTO [order] (orderId, userId, userName, totalPrice, dateTime, takeAway, isDone, remark) VALUES (N'OD1670121729628', 6, N'楓K', 155, N'2022/12/4', N'False', N'True', NULL);
INSERT INTO [order] (orderId, userId, userName, totalPrice, dateTime, takeAway, isDone, remark) VALUES (N'OD1670122266392', 7, N'章魚哥', 70, N'2022/12/4', N'True', N'True', NULL);
INSERT INTO [order] (orderId, userId, userName, totalPrice, dateTime, takeAway, isDone, remark) VALUES (N'OD1670122368461', 8, N'珊迪', 140, N'2022/12/4', N'True', N'True', NULL);
INSERT INTO [order] (orderId, userId, userName, totalPrice, dateTime, takeAway, isDone, remark) VALUES (N'OD1670122492366', 9, N'派大星', 215, N'2022/12/4', N'True', N'False', NULL);
GO


INSERT INTO [subCategory] (subCatId, subCatName, isMulti) VALUES (N'AH01', N'餐點特製', N'True');
INSERT INTO [subCategory] (subCatId, subCatName, isMulti) VALUES (N'AH02', N'大小', N'False');
INSERT INTO [subCategory] (subCatId, subCatName, isMulti) VALUES (N'AH03', N'溫度', N'False');
INSERT INTO [subCategory] (subCatId, subCatName, isMulti) VALUES (N'AH04', N'醬料', N'True');
GO


SET IDENTITY_INSERT [dbo].[subjoin] ON 
INSERT INTO [subjoin] (subId, subCatId, subName, subPrice) VALUES (1, N'AH01', N'減油', 0);
INSERT INTO [subjoin] (subId, subCatId, subName, subPrice) VALUES (2, N'AH01', N'減鹽', 0);
INSERT INTO [subjoin] (subId, subCatId, subName, subPrice) VALUES (3, N'AH01', N'減糖', 0);
INSERT INTO [subjoin] (subId, subCatId, subName, subPrice) VALUES (4, N'AH02', N'M', 0);
INSERT INTO [subjoin] (subId, subCatId, subName, subPrice) VALUES (5, N'AH02', N'L', 10);
INSERT INTO [subjoin] (subId, subCatId, subName, subPrice) VALUES (6, N'AH03', N'熱', 0);
INSERT INTO [subjoin] (subId, subCatId, subName, subPrice) VALUES (7, N'AH03', N'溫', 0);
INSERT INTO [subjoin] (subId, subCatId, subName, subPrice) VALUES (8, N'AH03', N'去冰', 0);
INSERT INTO [subjoin] (subId, subCatId, subName, subPrice) VALUES (9, N'AH03', N'冰', 0);
INSERT INTO [subjoin] (subId, subCatId, subName, subPrice) VALUES (10, N'AH04', N'紅酒醬', 0);
INSERT INTO [subjoin] (subId, subCatId, subName, subPrice) VALUES (11, N'AH04', N'胡椒醬', 0);
INSERT INTO [subjoin] (subId, subCatId, subName, subPrice) VALUES (12, N'AH04', N'奶油蘑菇醬', 0);
INSERT INTO [subjoin] (subId, subCatId, subName, subPrice) VALUES (13, N'AH04', N'荷蘭醬', 0);
INSERT INTO [subjoin] (subId, subCatId, subName, subPrice) VALUES (14, N'AH04', N'胡麻醬', 0);
INSERT INTO [subjoin] (subId, subCatId, subName, subPrice) VALUES (15, N'AH04', N'糖醋醬', 0);
INSERT INTO [subjoin] (subId, subCatId, subName, subPrice) VALUES (16, N'AH04', N'番茄醬', 0);
SET IDENTITY_INSERT [dbo].[subjoin] OFF
GO


SET IDENTITY_INSERT [dbo].[users] ON 
INSERT INTO [users] (userId, userName, password, phone, email, role) VALUES (2, N'薯餅', N'0000', N'912345678', N'potato@gmail.com', N'admin');
INSERT INTO [users] (userId, userName, password, phone, email, role) VALUES (3, N'小明', N'0000', N'958783183', N'cake@gmail.com', N'customer');
INSERT INTO [users] (userId, userName, password, phone, email, role) VALUES (4, N'大明', N'0000', N'911333555', N'ming@gmail.com', N'user');
INSERT INTO [users] (userId, userName, password, phone, email, role) VALUES (5, N'阿姨', N'0000', N'988168168', N'anti@gmail.com', N'admin');
INSERT INTO [users] (userId, userName, password, phone, email, role) VALUES (6, N'杰倫', N'0000', N'926398045', N'jay@gmail.com', N'customer');
INSERT INTO [users] (userId, userName, password, phone, email, role) VALUES (7, N'楓K', N'0000', N'911321123', N'karen@gmail.com', N'customer');
INSERT INTO [users] (userId, userName, password, phone, email, role) VALUES (8, N'章魚哥', N'0000', N'911123123', N'squidward@gmail.com', N'customer');
INSERT INTO [users] (userId, userName, password, phone, email, role) VALUES (9, N'珊迪', N'0000', N'912123222', N'sandy@gmail.com', N'customer');
INSERT INTO [users] (userId, userName, password, phone, email, role) VALUES (10, N'派大星', N'0000', N'911123333', N'patrick@gmail.comcustomer', N'');
INSERT INTO [users] (userId, userName, password, phone, email, role) VALUES (11, N'A2', N'null', N'9', N'A2@store.com', N'insider');
INSERT INTO [users] (userId, userName, password, phone, email, role) VALUES (12, N'A3', N'null', N'9', N'A3@store.com', N'insider');
INSERT INTO [users] (userId, userName, password, phone, email, role) VALUES (13, N'A4', N'null', N'9', N'A4@store.com', N'insider');
SET IDENTITY_INSERT [dbo].[users] OFF
GO