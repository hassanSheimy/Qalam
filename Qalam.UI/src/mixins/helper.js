import axios from 'axios'

export default {
    data: () => ({
        user: {
            name: '',
            fullName: '',
            roles: [],
            token: ''
        },
        requestCount: 0
    }),

    methods: {
        GetUserMinimalDetails(userData){
            return {
                'name': userData.userName || '',
                'full-name': userData.fullName || '',
                'roles': userData.roles.map(r => r.name) || [],
                'token': userData.token
            }
        },
        ShortSession(userData){
            var user = this.GetUserMinimalDetails(userData);
            sessionStorage.setItem('user', JSON.stringify(user));
        },
        LongSession(userData){
            var user = this.GetUserMinimalDetails(userData);
            localStorage.user = JSON.stringify(user);
        },
        UpdateUser(userData){
            var user = {};
            if(localStorage.user){
                user = JSON.parse(localStorage.user);
                Object.assign(user, userData);
                localStorage.removeItem("user");
                localStorage.user = JSON.stringify(user);
            }
            else if(sessionStorage.getItem('user')){
                user = JSON.parse(sessionStorage.getItem('user'));
                Object.assign(user, userData);
                sessionStorage.removeItem('user');
                sessionStorage.setItem('user', JSON.stringify(user));
            }
        },

        IsThereUser(){
            var flag = true;
            var user = {};
            if(localStorage.user){
                user = JSON.parse(localStorage.user);
            }
            else if(sessionStorage.getItem('user')){
                user = JSON.parse(sessionStorage.getItem('user'));
            }
            if(user['name'] && user['roles'] && user['token']){
                this.user.name = user['name'];
                this.user.fullName = user['full-name'];
                this.user.roles = user['roles'];
                this.user.token = user['token'];
            }
            else {
                flag = this.ResetUserData();
            }

            if(flag){
                for(var i = 0; i < this.user.roles.length; i++){
                    this.user.roles[i] = this.user.roles[i].toLowerCase();
                }
            }

            return flag;
        },

        Logout(){
            this.ResetUserData()
            return false;
        },

        ResetUserData(){
            this.user.name = '';
            this.user.roles = [];
            this.user.token = '';
            localStorage.removeItem("user")
            sessionStorage.removeItem('user');
            this.$store.commit("UpdateUserData", this.user);
            return false;
        },

        HttpRequest(requestOptions, onUploadProgress = (e) => {}){
            var options = {
                url: `${this.$BACKEND_APP_URL}/${requestOptions.url}`,
                method: requestOptions.method || 'GET',
                timeout: requestOptions.timeout || 500000,
                contentType: requestOptions.contentType || 'application/json; charset=utf-8',
                dataType: requestOptions.dataType || 'json',
                params: requestOptions.params || null,
                data: requestOptions.data || null,
                headers: requestOptions.headers || {},
                auth: requestOptions.auth || false,
                successNotify: requestOptions.successNotify || false,
                faildNotify: typeof requestOptions.faildNotify === "undefined" ? true : requestOptions.faildNotify,
                loading: typeof requestOptions.loading === "undefined" ? true : requestOptions.loading,
                autoRedirect: typeof requestOptions.autoRedirect === "undefined" ? true : requestOptions.autoRedirect
            }

            if(options.loading){
                this.requestCount++;
                this.$eventBus.$emit("start-loading");
            }
            
            options.headers['Content-Type'] = 'application/json; charset=utf-8';

            if(!options.data)
                delete options.data;
            if(!options.params)
                delete options.params;
            // set Authrization in header
            if(options.auth){
                if(!options.headers)
                    options.headers = {}
                this.IsThereUser();
                options.headers.Authorization = `Bearer ${this.user.token}`;
            }

            return new Promise((resolve, rejection) => {
                axios({
                    url: options.url,
                    method: options.method,
                    timeout: options.timeout,
                    contentType: options.contentType,
                    dataType: options.dataType,
                    data: options.data,
                    params: options.params,
                    headers: options.headers,
                    onUploadProgress: onUploadProgress
                }).then(function (response) {
                    // handle success
                    console.log(response);
                    if(response && response.data && response.data.isSucceeded){
                        if(options.successNotify){
                            // show success notification
                            this.Notify(this.$locales.t('defaultSuccessNotification'))
                        }
                        resolve(response.data);
                    }
                    // error happened
                    else if(options.faildNotify){
                        // show error notification
                        this.Notify(this.$locales.t('defaultFaildNotification'))
                        rejection(response);
                    }
                }.bind(this)).catch(function (error) {
                    // handle error
                    if(options.autoRedirect && error.response){
                        switch(error.response.status){
                            case 401:
                                this.Unauthorized(this.$route.fullPath);
                                break;
                            case 500:
                                this.InternalError();
                                break;
                        }
                    }
                    if(options.faildNotify){
                        // show error notification
                        this.Notify(this.$locales.t('defaultFaildNotification'))
                    }
                    rejection(error);
                    console.log(error);
                }.bind(this)).finally(function () {
                    // always executed
                    if(options.loading){
                        this.requestCount--;
                        if(this.requestCount == 0)
                            this.$eventBus.$emit("stop-loading")
                    }
                }.bind(this));
            });
        },

        // Get Random Unique Id
        RandomUniqueString() {
            return Math.random().toString(36).substring(2, 15) + Math.random().toString(36).substring(2, 15);
        },

        Unauthorized(returnUrl) {
            this.$router.push({
                name: 'unauthorized',
                query: { returnUrl: returnUrl }
            })
        },

        InternalError() {
            this.$router.push({
                name: 'server-error'
            })
        },

        RedirectHome(){
            this.$router.push({
                name: 'home'
            })
        },

        // Check if document is on fullscreen or not
        IsFullScreen() {
            return document.fullScreen || document.mozFullScreen || document.webkitIsFullScreen;
        },

        // Make element fullscreen
        OpenFullScreen(element) {
            if (this.IsFullScreen()) {
                return;
            }
            
            if (element == null) {
                return;
            }
            if (element.requestFullscreen) {
                element.requestFullscreen();
            } else if (element.mozRequestFullScreen) {
                element.mozRequestFullScreen();
            } else if (element.webkitRequestFullscreen) {
                element.webkitRequestFullscreen();
            } else if (element.msRequestFullscreen) {
                element.msRequestFullscreen();
            } else {
                //
            }
        },

        // Close fullscreen
        ExitFullscreen(){
            if (!this.IsFullScreen()) {
                return;
            }
            if (document.exitFullscreen) {
                document.exitFullscreen();
            } else if (document.mozCancelFullScreen) {
                document.mozCancelFullScreen();
            } else if (document.webkitExitFullscreen) {
                document.webkitExitFullscreen();
            }
        },

        Notify(notification){
            if(notification.audio){
                var audio = new Audio(require('@/assets/notification-sound.ogg'))
                audio.play().then(() => {
                    // play done successfuly
                }).catch(e => {
                    // error happened while play the audio
                });
            }
            this.$notify({
                group: notification.group || 'Qalam',
                title: notification.title,
                text: notification.text,
                type: notification.type || 'primary',
                duration: notification.duration || 6000,
                closeOnClick: notification.closeOnClick || true
            });
        },
    },

    directives: {
        'click-outside': {
            bind: function(el, binding, vNode) {
                // Provided expression must evaluate to a function.
                if (typeof binding.value !== 'function') {
                    const compName = vNode.context.name
                    let warn = `[Vue-click-outside:] provided expression '${binding.expression}' is not a function, but has to be`
                    if (compName) { warn += `Found in component '${compName}'` }
                    
                    console.error(warn);
                }
                // Define Handler and cache it on the element
                const bubble = binding.modifiers.bubble
                const handler = (e) => {
                    if (bubble || (!el.contains(e.target) && el !== e.target)) {
                        binding.value(e)
                    }
                }
                el.__vueClickOutside__ = handler
                // add Event Listeners
                document.addEventListener('click', handler)
            },
            
            unbind: function(el, binding) {
                // Remove Event Listeners
                document.removeEventListener('click', el.__vueClickOutside__)
                el.__vueClickOutside__ = null
            }
        }
    },
}