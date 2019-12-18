﻿var app = window.AddressSeleclList =
{
    AdrressArray: [
        ['基隆市', '200仁愛區', '201信義區', '202中正區', '203中山區', '204安樂區', '205暖暖區', '206七堵區'],
        ['台北市', '100中正區', '103大同區', '104中山區', '105松山區', '106大安區', '108萬華區', '110信義區', '111士林區', '112北投區', '114內湖區', '115南港區', '116文山區'],
        ['新北市', '207萬里鄉', '208金山鄉', '220板橋市', '221汐止市', '222深坑鄉', '223石碇鄉', '224瑞芳鎮', '226平溪鄉', '227雙溪鄉', '228貢寮鄉', '231新店市', '232坪林鄉', '233烏來鄉', '234永和市', '235中和市', '236土城市', '237三峽鎮', '238樹林市', '239鶯歌鎮', '241三重市', '242新莊市', '243泰山鄉', '244林口鄉', '247蘆洲市', '248五股鄉', '248新莊市', '249八里鄉', '251淡水鎮', '252三芝鄉', '253石門鄉'],
        ['桃園市', '320中壢市', '324平鎮市', '325龍潭鄉', '326楊梅鎮', '327新屋鄉', '328觀音鄉', '330桃園市', '333龜山鄉', '334八德市', '335大溪鎮', '336復興鄉', '337大園鄉', '338蘆竹鄉'],
        ['台中市', '400中區', '401東區', '402南區', '403西區', '404北區', '406北屯區', '407西屯區', '408南屯區', '411太平市', '412大里市', '413霧峰鄉', '414烏日鄉', '420豐原市', '421后里鄉', '422石岡鄉', '423東勢鎮', '424和平鄉', '426新社鄉', '427潭子鄉', '428大雅鄉', '429神岡鄉', '432大肚鄉', '433沙鹿鎮', '434龍井鄉', '435梧棲鎮', '436清水鎮', '437大甲鎮', '438外埔鄉', '439大安鄉'],
        ['台東縣', '950台東市', '951綠島鄉', '952蘭嶼鄉', '953延平鄉', '954卑南鄉', '955鹿野鄉', '956關山鎮', '957海端鄉', '958池上鄉', '959東河鄉', '961成功鎮', '962長濱鄉', '963太麻里鄉', '964金峰鄉', '965大武鄉', '966達仁鄉'],
        ['台南市', '700中西區', '701東區', '702南區', '704北區', '708安平區', '709安南區', '710永康市', '711歸仁鄉', '712新化鎮', '713左鎮鄉', '714玉井鄉', '715楠西鄉', '716南化鄉', '717仁德鄉', '718關廟鄉', '719龍崎鄉', '720官田鄉', '721麻豆鎮', '722佳里鎮', '723西港鄉', '724七股鄉', '725將軍鄉', '726學甲鎮', '727北門鄉', '730新營市', '731後壁鄉', '732白河鎮', '733東山鄉', '734六甲鄉', '735下營鄉', '736柳營鄉', '737鹽水鎮', '741善化鎮', '741新市鄉', '742大內鄉', '743山上鄉', '744新市鄉', '745安定鄉'],
        ['宜蘭縣', '260宜蘭市', '261頭城鎮', '262礁溪鄉', '263壯圍鄉', '264員山鄉', '265羅東鎮', '266三星鄉', '267大同鄉', '268五結鄉', '269冬山鄉', '270蘇澳鎮', '272南澳鄉', '290釣魚台'],
        ['花蓮縣', '970花蓮市', '971新城鄉', '972秀林鄉', '973吉安鄉', '974壽豐鄉', '975鳳林鎮', '976光復鄉', '977豐濱鄉', '978瑞穗鄉', '979萬榮鄉', '981玉里鎮', '982卓溪鄉', '983富里鄉'],
        ['金門縣', '890金沙鎮', '891金湖鎮', '892金寧鄉', '893金城鎮', '894烈嶼鄉', '896烏坵鄉'],
        ['南投縣', '540南投市', '541中寮鄉', '542草屯鎮', '544國姓鄉', '545埔里鎮', '546仁愛鄉', '551名間鄉', '552集集鎮', '553水里鄉', '555魚池鄉', '556信義鄉', '557竹山鎮', '558鹿谷鄉'],
        ['苗栗縣', '350竹南鎮', '351頭份鎮', '352三灣鄉', '353南庄鄉', '354獅潭鄉', '356後龍鎮', '357通霄鎮', '358苑裡鎮', '360苗栗市', '361造橋鄉', '362頭屋鄉', '363公館鄉', '364大湖鄉', '365泰安鄉', '366銅鑼鄉', '367三義鄉', '368西湖鄉', '369卓蘭鎮'],
        ['高雄市', '800新興區', '801前金區', '802苓雅區', '803鹽埕區', '804鼓山區', '805旗津區', '806前鎮區', '807三民區', '811楠梓區', '812小港區', '813左營區', '817東沙群島', '819南沙群島', '814仁武鄉', '815大社鄉', '820岡山鎮', '821路竹鄉', '822阿蓮鄉', '823田寮鄉', '824燕巢鄉', '825橋頭鄉', '826梓官鄉', '827彌陀鄉', '828永安鄉', '829湖內鄉', '830鳳山市', '831大寮鄉', '832林園鄉', '833鳥松鄉', '840大樹鄉', '842旗山鎮', '843美濃鎮', '844六龜鄉', '845內門鄉', '846杉林鄉', '847甲仙鄉', '848桃源鄉', '849那瑪夏鄉', '851茂林鄉', '852茄萣鄉'],
        ['雲林縣', '630斗南鎮', '631大埤鄉', '632虎尾鎮', '633土庫鎮', '634褒忠鄉', '635東勢鄉', '636台西鄉', '637崙背鄉', '638麥寮鄉', '640斗六市', '643林內鄉', '646古坑鄉', '647莿桐鄉', '648西螺鎮', '649二崙鄉', '651北港鎮', '652水林鄉', '653口湖鄉', '654四湖鄉', '655元長鄉'],
        ['新竹市', '300北區', '300東區', '300香山區'],
        ['新竹縣', '300寶山鄉', '302竹北市', '303湖口鄉', '304新豐鄉', '305新埔鎮', '306關西鎮', '307芎林鄉', '308寶山鄉', '310竹東鎮', '311五峰鄉', '312橫山鄉', '313尖石鄉', '314北埔鄉', '315峨眉鄉'],
        ['嘉義市', '600西區', '600東區'],
        ['嘉義縣', '602番路鄉', '603梅山鄉', '604竹崎鄉', '605阿里山鄉', '606中埔鄉', '607大埔鄉', '608水上鄉', '611鹿草鄉', '612太保市', '613朴子市', '614東石鄉', '615六腳鄉', '616新港鄉', '621民雄鄉', '622大林鎮', '623溪口鄉', '624義竹鄉', '625布袋鎮'],
        ['彰化縣', '500彰化市', '502芬園鄉', '503花壇鄉', '504秀水鄉', '505鹿港鎮', '506福興鄉', '507線西鄉', '508和美鎮', '509伸港鄉', '510員林鎮', '511社頭鄉', '512永靖鄉', '513埔心鄉', '514溪湖鎮', '515大村鄉', '516埔鹽鄉', '520田中鎮', '521北斗鎮', '522田尾鄉', '523埤頭鄉', '524溪州鄉', '525竹塘鄉', '526二林鎮', '527大城鄉', '528芳苑鄉', '530二水鄉'],
        ['屏東縣', '900屏東市', '901三地門鄉', '902霧台鄉', '903瑪家鄉', '904九如鄉', '905里港鄉', '906高樹鄉', '907鹽埔鄉', '908長治鄉', '909麟洛鄉', '911竹田鄉', '912內埔鄉', '913萬丹鄉', '920潮州鎮', '921泰武鄉', '922來義鄉', '923萬巒鄉', '924崁頂鄉', '925新埤鄉', '926南州鄉', '927林邊鄉', '928東港鎮', '929琉球鄉', '931佳冬鄉', '932新園鄉', '940枋寮鄉', '941枋山鄉', '942春日鄉', '943獅子鄉', '944車城鄉', '945牡丹鄉', '946恆春鎮', '947滿州鄉'],
        ['澎湖縣', '880馬公市', '881西嶼鄉', '882望安鄉', '883七美鄉', '884白沙鄉', '885湖西鄉'],
        ['連江縣', '209南竿鄉', '210北竿鄉', '211莒光鄉', '212東引鄉']
    ]
    ,

    defaultOptionCityText: '請選擇縣市',
    defaultOptionCityValue: '',
    defaultOptionAreaText: '請選擇鄉鎮',
    defaultOptionAreaValue: '',

    initialize: function (city, area, defaultCityText, defaultCityValue, defaultAreaText, defaultAreaValue) {

        var cityText = defaultCityText ? defaultCityText : this.defaultOptionCityText;
        var cityValue = defaultAreaValue ? defaultAreaValue : this.defaultOptionCityValue;
        var areaText = defaultAreaText ? defaultAreaText : this.defaultOptionAreaText;
        var areaValue = defaultAreaValue ? defaultAreaValue : this.defaultOptionAreaValue;

        var citySelect = document.getElementById(city);
        var areaSelect = document.getElementById(area);

        if (typeof citySelect === 'object' && citySelect === null) {
            return false;
        }

        citySelect.options[0] = new Option(cityText, cityValue);
        areaSelect.options[0] = new Option(areaText, areaValue);
        for (var i = 0; i < this.AdrressArray.length; i++) {
            citySelect.options[i + 1] = new Option(this.AdrressArray[i][0], this.AdrressArray[i][0]);
        }
        citySelect.addEventListener ? citySelect.addEventListener('change', function (e) { app.AppendArea(e, areaSelect, areaText, areaValue) }, false) : citySelect.attachEvent('onchange', function (e) { app.AppendArea(e, areaSelect, areaText, areaValue) });
    },

    AppendArea: function (e, AreaSelect, areaText, areaValue) {
        var target = e.target ? e.target : e.srcElement;
        if (target.selectedIndex == 0) {
            AreaSelect.options.length = 0;
            AreaSelect.options[0] = new Option(areaText, areaValue);
            return;
        }
        AreaSelect.options.length = this.AdrressArray[target.selectedIndex - 1].length - 1;
        for (var i = 1; i < this.AdrressArray[target.selectedIndex - 1].length; i++) {
            AreaSelect.options[i - 1] = new Option(this.AdrressArray[target.selectedIndex - 1][i], this.AdrressArray[target.selectedIndex - 1][i]);
        }
    },

    ReturnSelectAddress: function (city, area) {
        var city = document.getElementById(city);
        var area = document.getElementById(area);
        return city.value + area.value;
    }
};

window.onload = function () {
    //當頁面載完之後，用AddressSeleclList.Initialize()，
    //傳入要綁定的縣市下拉選單ID及鄉鎮市區下拉選單ID
    app.initialize('city', 'area');
}
