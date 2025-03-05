
//讀取購物車內容
export function getCarts() {
	if(localStorage.getItem('cart'))
		return JSON.parse(localStorage.getItem('cart'));
	else
		return []	
}

//save data in local storage
export function saveDataToLocalStorage(key, data) {
	localStorage.setItem(key, JSON.stringify(data));
}
//get data from local storage
export function getDataFromLocalStorage(key) {
	if(localStorage.getItem(key))
		return JSON.parse(localStorage.getItem(key));
	else
		return undefined
}
//delete data from local storage
export function deleteDataFromLocalStorage(key) {
	localStorage.removeItem(key);
}
// 取得當前時間(2022-01-01 00:00:00)
export function getTimeNow() {
	let d = new Date();
	const theTime = d.getFullYear() + "-" + (d.getMonth() + 1).AddZero() + "-" + d.getDate().AddZero() + " " + d.getHours().AddZero() + ":" + d.getMinutes().AddZero() + ":" + d.getSeconds().AddZero();
	return theTime;
};
