function AddNewTime() {
    const selectedTimezoneId = document.getElementById('zone').value;
    fetch('/Home/Index?SelectedTimezoneId=' + selectedTimezoneId, {
        method: 'POST',
        body: JSON.stringify({
            TimeZone: this.TimeZone,
            Time: this.Time,
            ClientIp: this.ClientIp,
            UTCTime: this.UTCTime,
            CurrentTimeQueryId: this.CurrentTimeQueryId
        }),
        headers: {
            'Content-type': 'application/json'
        }
    })
        .then(function (response) {
            if (response.status != 200) {
                console.log('Something went wrong. Status code: ' + response.status);
                return;
            }
            response.json().then(function (currentTimeData) {
                console.log(currentTimeData)

                // new entry object
                const newTimezoneInfo = currentTimeData.newEntry;
                // all entries from database : CurrentTimeController / API 
                const entries = currentTimeData.allEntries;
                // add new entry to top of the list
                entries.reverse();
                // timezone info for new entry
                const timeZone = newTimezoneInfo.timeZone;
                // timezone and time display  
                const currentTime = moment(newTimezoneInfo.time).format('lll');

                document.getElementById('currentTimeText').textContent = 'The time in ' + timeZone + ' is: ' + currentTime;

                DisplayTimes(entries);
            });
        })
        .catch(function (err) {
            console.log('Fetch error', err);
        });

}


function AllEntries() {
    fetch('Home/GetRequestedTimes')
        .then(
            function (response) {
                if (response.status != 200) {
                    console.log('Something went wrong. Status code: ' + response.status);
                    return;
                }
                response.json().then(function (myData) {

                    myData.reverse();

                    DisplayTimes(myData)

                });
            })
        .catch(function (err) {
            console.log("Fetch error", err);
        });
}


function DisplayTimes(entries) {

    document.getElementById('timezoneDiv').innerHTML = "";

    entries.forEach(timezone => {

        const timeDiv = document.getElementById('timezoneDiv')

        const timezoneList = document.createElement('ul')
        timezoneList.setAttribute('class', 'timezoneList')

        // Timezone Name   
        const timezoneName = document.createElement('li')
        timezoneName.setAttribute('class', 'timezoneName')
        timezoneName.textContent = `Timezone: ${timezone.timeZone}`;

        //Timezone Time
        const timezoneTime = document.createElement('li')
        timezoneTime.setAttribute('class', 'timezoneTime')
        timezoneTime.textContent = `Time: ${moment(timezone.time).format('lll')}`;

        // Timezone ClientIP
        const timezoneClientIp = document.createElement('li')
        timezoneClientIp.setAttribute('class', 'timezoneCleintIp')
        timezoneClientIp.textContent = `ClientIp: ${timezone.clientIp}`;

        // Timezone UTCTime
        const timezoneUTCTime = document.createElement('li')
        timezoneUTCTime.setAttribute('class', 'timezoneUTCTime')
        timezoneUTCTime.textContent = `UTCTime: ${moment(timezone.utcTime).format('lll')}`;

        // Timezone CurrentTimeQueryId
        const timezoneCurrentTimeQueryId = document.createElement('li')
        timezoneCurrentTimeQueryId.setAttribute('class', 'timezoneCurrentTimeQueryId')
        timezoneCurrentTimeQueryId.textContent = `CurrentTimeQueryId: ${timezone.currentTimeQueryId}`;

        timeDiv.appendChild(timezoneList)

        timezoneList.appendChild(timezoneName)
        timezoneList.appendChild(timezoneTime)
        timezoneList.appendChild(timezoneClientIp)
        timezoneList.appendChild(timezoneUTCTime)
        timezoneList.appendChild(timezoneCurrentTimeQueryId)

    })
}

