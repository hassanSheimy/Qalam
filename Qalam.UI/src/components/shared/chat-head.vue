<template>
    <!-- Chat Head -->
    <div class="circular-menu-container" :class="{right: position !== 'left'}">
        <v-badge v-if="unreadMessages > 0" color="secondary" :content="unreadMessages"></v-badge>
        <button class="circular-menu-button primary" v-on:click="open">
            <i class="fas fa-comment" v-if="!opened"></i>
            <i class="fas fa-times" v-else></i>
        </button>
        <transition name="slide" mode="out-in">
            <div class="menu-container" :class="{opened: opened}" v-if="opened">
                <div id="liveChat" class="live-chat position-fixed" :class="{right: position !== 'left'}">
                    <header class="clearfix primary darken-1">
                        <h4>{{title}}</h4>
                    </header>

                    <div class="chat">
                        <div class="message-list">
                            <div class="chat-history">
                                <div v-for="(message, index) in messages" :key="'message-content-' + index">
                                    <div class="chat-message clearfix">
                                        <div class="chat-message-content clearfix" style="text-align: right">
                                            <span class="chat-time">{{message.time}}</span>
                                            <h5>{{message.senderName}}</h5>
                                            <p>{{message.content}}</p>
                                        </div> <!-- end chat-message-content -->
                                    </div> <!-- end chat-message -->
                                    <hr>
                                </div>
                            </div> <!-- end chat-history -->
                        </div>
                        <v-form v-show="write" class="justify-center">
                            <v-text-field
                                v-model="messageText"
                                @keydown.enter="SendMessage"
                                append-icon="fas fa-paper-plane"
                                type="text"
                                :label="$locales.t('writeMessage')"
                                @click:append="SendMessage"
                            ></v-text-field>
                            <v-text-field v-show="false" />
                        </v-form>
                    </div> <!-- end chat -->
                </div>
            </div>
        </transition>
    </div> <!-- end live-chat -->
</template>

<script>
export default {
    name: "ChatHead",

    props: {
        distance: {
            type: Number,
            default: 100        
        },
        position: {
            type: String,
            default: 'left'
        },
        scale: {
            type: Number,
            default: 1
        },
        title: {
            type: String,
            required: true
        },
        write: {
            type: Boolean,
            default: false
        }
    },

    data: () => ({
        opened: false,

        messageText: null,
        
        messages: [],

        unreadMessages: 0
    }),

    mounted(){
        if(this.IsThereUser()){
            this.$eventBus.$on("newMessage", this.NewMessage);
        }
    },

    methods: {
        open: function() {
            this.opened = !this.opened; 
            this.unreadMessages = 0; 
        },
        SendMessage(){
            this.messageText = this.messageText.replace(/^\s+|\s+$/g, '');
            if(this.messageText){
                var message = {
                    time: new Date().toLocaleTimeString(),
                    senderName: this.user.name,
                    content: this.messageText
                }
                this.messages.push(message);
                this.messageText = null;
                this.$emit("send", message);
            }
        },
        NewMessage(message){
            this.messages.push(message);
            if(!this.opened){
                this.unreadMessages++;
            }
        }
    }
}
</script>

<style scoped>
.circular-menu-button {
    z-index: 100;
    -webkit-transition: 0.3s cubic-bezier(0, 0, 0.2, 1);
    -moz-transition: 0.3s cubic-bezier(0, 0, 0.2, 1);
    -o-transition: 0.3s cubic-bezier(0, 0, 0.2, 1);
    transition: 0.3s cubic-bezier(0, 0, 0.2, 1);
    display: flex;
    align-items: center;
    justify-content: center;
    cursor: pointer;
    border-style: none;
    outline: 0;
    height: 64px;
    width: 64px;
    background-color: #546e7a;
    border-radius: 100%;
    box-shadow: 0 1px 3px rgba(0,0,0,0.12), 0 1px 2px rgba(0,0,0,0.24);
}
.circular-menu-button:active {
    box-shadow: 0 3px 6px rgba(0,0,0,0.16), 0 3px 6px rgba(0,0,0,0.23);
}
.circular-menu-button i {
    color: #fff;
    font-size: 30px;
}
.circular-menu-container {
    z-index: 100;
    position: fixed;
    top: auto;
    left: 16px;
    bottom: 16px;
}
.circular-menu-container.right {
    right: 16px;
    left: initial;
}
.circular-menu-container>.menu-container > * {
    -webkit-transition:all 1.0s ease-in-out;
    -moz-transition:all 1.0s ease-in-out;
    -o-transition:all 1.0s ease-in-out;
    transition:all 1.0s ease-in-out;
    position: absolute;
    z-index: -1;
}
.live-chat {
    -webkit-transition:all 1.0s ease-in-out;
    -moz-transition:all 1.0s ease-in-out;
    -o-transition:all 1.0s ease-in-out;
    transition:all 1.0s ease-in-out;
    bottom: 50px;
    font-size: 12px;
    left: 16px;
    width: 280px;
    z-index: 2
}

.live-chat.right{
    right: 16px;
    left: initial;
}

.live-chat header {
    border-radius: 5px 5px 0 0;
    color: #fff;
    cursor: pointer;
    padding: 16px 24px;
}

.live-chat h4 {
    font-size: 15px;
}

.live-chat h5 {
    font-size: 13px;
    font-weight: bolder
}

.live-chat p {
    font-size: 13px;
    font-weight: bold
}

.live-chat form {
    padding: 10px 24px;
}

.live-chat input[type="text"] {
    border: 1px solid #ccc;
    border-radius: 3px;
    padding: 8px;
    outline: none;
}

.chat {
    background: #f0f0f0;
    border-left: double 1px black;
    border-right: double 1px black;
    border-bottom: double 1px black;
}

.chat-history {
    padding: 8px 24px;
    overflow-y: scroll;
    overflow-x: hidden;
}

.message-list {
    max-height: 60vh;
    min-height: min(350px, 60vh);
    overflow-y: scroll;
    overflow-x: hidden;
}

.slide-left-enter-active,
.slide-left-leave-active,
.slide-right-enter-active,
.slide-right-leave-active {
  transition-duration: 0.5s;
  transition-property: height, opacity, transform;
  transition-timing-function: cubic-bezier(0.55, 0, 0.1, 1);
  overflow: hidden;
}

.slide-left-enter,
.slide-right-leave-active {
  opacity: 0;
  transform: translate(2em, 0);
}

.slide-left-leave-active,
.slide-right-enter {
  opacity: 0;
  transform: translate(-2em, 0);
}
</style>