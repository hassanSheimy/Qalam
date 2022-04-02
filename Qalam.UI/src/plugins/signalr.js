import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'

export default {
    install (Vue) {
        var userName = '';
        if(localStorage.user){
            userName = JSON.parse(localStorage.user)['name']
        }

        const hubConnection = new HubConnectionBuilder()
            .withUrl(`${Vue.prototype.$BACKEND_APP_URL}/hub/qalam-real-time?user-name=${userName}`)
            .configureLogging(LogLevel.None)
            .withAutomaticReconnect([0, 2000, 10000, 30000, 60000])
            .build();

        let startedPromise = null;
        let connectionTimes = 0;
        let manuallyClosed = false;
        let waitTHreshold = 20000;
        let hubConnected = false;

        const qalamHub = new Vue() 
        Vue.prototype.$qalamHub = qalamHub

        function start () {
            hubConnection.start().then(() => {
                hubConnected = true;
                qalamHub.$emit('connected');
            }).catch(err => {
                // error happened
            })
        }

        hubConnection.onclose(() => {
            if (!manuallyClosed) start()
        })

        // received functions
        hubConnection.on('ExistingUser', () => {
            qalamHub.$emit('existing-user');
        });
        hubConnection.on('LiveTeacherToggleMessage', (isMessageOn) => {
            qalamHub.$emit('live-teacher-toggle-message', isMessageOn);
        });
        hubConnection.on('LiveStudentMessage', (message) => {
            qalamHub.$emit('live-receive-message', message)
        });
        hubConnection.on('LiveStudentLike', (studentName) => {
            qalamHub.$emit('live-student-like', studentName);
        });
        hubConnection.on('LiveStudentRaiseHand', (studentName) => {
            qalamHub.$emit('live-student-raise-hand', studentName);
        });
        hubConnection.on('LiveStudentRepeat', (studentName) => {
            qalamHub.$emit('live-student-repeat', studentName);
        });
        hubConnection.on('UserJoinGroup', (userName, count) => {
            qalamHub.$emit('user-join-group', userName, count);
        });
        hubConnection.on('NoGroupHost', () => {
            qalamHub.$emit('no-group-host');
        });
        hubConnection.on('UserLeaveGroup', (userName, count) => {
            qalamHub.$emit('user-leave-group', userName, count);
        });
        hubConnection.on('Notifications', (notifications) => {
            console.log(notifications);
            qalamHub.$emit('user-receive-notifications', notifications);
        });
        hubConnection.on('LiveEnded', () => {
            qalamHub.$emit('live-ended');
        })


        qalamHub.$on('hub-connected', () => {
            if(hubConnection.state == 'Connected' && hubConnected){
                qalamHub.$emit('connected');
            }
        });
        // invoked functions
        qalamHub.$on('disconnect', () => {
            if(hubConnection.state == 'Connected'){
                hubConnection.stop()
            }
        });
        qalamHub.$on('reconnect', (userName) => {
            if(hubConnection.state == 'Connected'){
                hubConnection.invoke('OnReconnectedAsync', userName);
            }
        });
        qalamHub.$on('join-group', (groupId, userName, isGroupHost, liveId) => {
            if(hubConnection.state == 'Connected'){
                hubConnection.invoke('JoinGroup', groupId, userName, isGroupHost, liveId);
            }
        });
        qalamHub.$on('live-toggle-messages', (isMessageOn, groupId) => {
            if(hubConnection.state == 'Connected'){
                hubConnection.invoke('LiveTeacherToggleMessage', isMessageOn, groupId);
            }
        });
        qalamHub.$on('live-send-message', (message, groupId) => {
            if(hubConnection.state == 'Connected'){
                hubConnection.invoke('LiveStudentMessage', message, groupId);
            }
        });
        qalamHub.$on('live-like', (studentName, groupId) => {
            if(hubConnection.state == 'Connected'){
                hubConnection.invoke('LiveStudentLike', studentName, groupId);
            }
        });
        qalamHub.$on('live-raise-hand', (studentName, groupId) => {
            if(hubConnection.state == 'Connected'){
                hubConnection.invoke('LiveStudentRaiseHand', studentName, groupId);
            }
        });
        qalamHub.$on('live-repeat', (studentName, groupId) => {
            if(hubConnection.state == 'Connected'){
                hubConnection.invoke('LiveStudentRepeat', studentName, groupId);
            }
        });
        qalamHub.$on('end-live', groupId => {
            if(hubConnection.state == 'Connected'){
                hubConnection.invoke('EndLive', groupId);
            }
        });

        // Start everything
        manuallyClosed = false
        start();
    }
}