window.onload = function initCityOnClick() {
    let cityLi = document.getElementsByClassName('set_city_manual')
    for (var i = 0; i < cityLi.length; i++){
        let a = i//just in case, not sure if necessary 
        let text = cityLi[a].innerHTML
        cityLi[a].onClick = setCityManualOnClick(text)
    }
    if (getCookie('city') === '')
        setCityAutomatically()
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
        //TODO show some popup asking if the city is correct
        //setCity(city)
        alert(city)//TODO do something useful with the city
    }
    else
        alert("Html error")
}

//Can be used to set city manually
function setCity(city){
    document.cookie = "city="+city
}

function setCityManualOnClick(text){
    setCity(text)
}