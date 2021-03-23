//artemgur
window.onload = function initCityOnClick() {
    let cityLi = document.getElementsByClassName('set_city_manual')
    for (var i = 0; i < cityLi.length; i++){
        let a = i//just in case, not sure if necessary
        let li = cityLi[a]
        let text = li.innerHTML
        li.onclick = function() {setCityManualOnClick(text)}
    }
    if (getCookie('city') === '')
       setCityAutomatically()
    else
        setCityInHeaderFromCookies()
}

//Gets cookie value by cookie name
const getCookie = (name) => (
    document.cookie.match('(^|;)\\s*' + name + '\\s*=\\s*([^;]+)')?.pop() || ''
)
//Source: https://stackoverflow.com/questions/5639346/what-is-the-shortest-function-for-reading-a-cookie-by-name-in-javascript

function setCityAutomatically(){
    if (navigator.geolocation)
        navigator.geolocation.getCurrentPosition(setCityAutomaticallyCallback)
    else
        alert("Geolocation isn't supported")
}

async function setCityAutomaticallyCallback(position) {
    let response = await fetch('https://api.bigdatacloud.net/data/reverse-geocode-client?latitude='
        + position.coords.latitude
        + '&longitude='
        + position.coords.longitude
        + '&localityLanguage=ru')
    if (response.ok){
        let json = await response.json()
        let city = json.city
        if (isCitySupported(city)) {
            document.getElementById('city_modal_title').innerHTML = 'Ваш город - ' + city + '?'
            document.getElementById('city_modal_yes').onclick = function () {
                setCity(city)//TODO check if the city is in some list of possible cities
                cityModalClose()
            }
            $('#city_modal').modal('show')
        }
    }
    else
        alert("Html error")
}

//Can be used to set city manually
function setCity(city){
    document.cookie = "city="+city
    setCityInHeaderFromCookies()
}

function setCityManualOnClick(text){
    setCity(text)
}

function cityModalClose(){
    $('#city_modal').modal('hide')
}

function setCityInHeaderFromCookies(){
    document.getElementById('city_name_header').innerHTML = getCookie('city')
}

function isCitySupported(city){
    let cityLi = document.getElementsByClassName('set_city_manual')
    for (var i = 0; i < cityLi.length; i++){
        let cityFromFrontend = cityLi[i].innerHTML
        if (cityFromFrontend === city)
            return true
    }
    return false
}