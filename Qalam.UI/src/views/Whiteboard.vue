<template>
    <div>
        <default-header />
        <v-container fluid class="mb-4 px-4 whiteboard-container">
            <v-row class="white-board" ref="sketchpad" :class="{dark: $vuetify.theme.isDark}" justify="center">
                <whiteboard-toolbar :buttons="buttons" />
                <chat-head :title="$locales.t('questionComments')" position="right" />
            </v-row>
        </v-container>
        <secondary-footer />
        <save-live-stream-name-modal :dialog="nameDialog" @done="ModalDone" @close="ModalClosed" :live-stream="liveStream" />
    </div>
</template>

<script>
import DefaultHeader from '@/components/layout/default-header.vue'
import CircularMenu from '@/components/shared/circular-menu.vue'
import ChatHead from '@/components/shared/chat-head.vue'
import Sketchpad from '@/assets/js/responsive-sketchpad.js'
import WhiteboardToolbar from '@/components/live-stream/whiteboard-toolbar.vue'
import SecondaryFooter from '@/components/layout/secondary-footer.vue'
import SaveLiveStreamNameModal from '@/components/modals/save-live-stream-name-modal.vue'

export default {
    name: "Whiteboard",

    components: {
        DefaultHeader,
        CircularMenu,
        ChatHead,
        WhiteboardToolbar,
        SecondaryFooter,
        SaveLiveStreamNameModal
    },

    data: () => ({
        nameDialog: false,
        pad: null,
        hubConnection: null,
        liveId: 0,
        whiteboardCursor: null,
        canSenMessage: true,
        lineColor: '#fff',
        numberOfStudent: 0,
        noLiveDialog: false,
        liveStream: null
    }),

    computed: {
        buttons() {
            return [
                { callback: this.PenDraw, icon: 'fas fa-pen' },
                { callback: this.ToggleChatIcon, icon: 'fas fa-tint' },
                { callback: this.RedoDraw, icon: 'fas fa-redo' },
                { callback: this.UndoDraw, icon: 'fas fa-undo' },
                { callback: this.ClearBoard, icon: 'fas fa-trash' },
                { callback: this.Erase, icon: 'fas fa-eraser' },
                { callback: this.ToggleFullScreen, icon: 'fas fa-compress' },
                { callback: this.EndLive, icon: 'fas fa-door-open' },
            ]
        }
    },

    mounted() {
        this.$eventBus.$emit('toggle-app-padding', false);
        this.liveId = this.$route.params.streamKey;
        this.$eventBus.$on('dark-mode', this.ChangeMode);
        if(this.IsThereUser()){
            //this.CheckStreamKey()
            this.StartWhiteboard();
            window.addEventListener('resize', this.HandelResize);
            this.InitSignalR();

        }
        else{
            this.Unauthorized(this.$route.fullPath);
        }
    },

    destroyed() {
        this.$eventBus.$emit('toggle-app-padding', true);
    },

    methods: {
        CheckStreamKey(){
            this.HttpRequest({
                url: 'api/live/checktime',
                auth: true
            }).then(r => {
                if(r.data){
                    this.liveStream = r.data;
                    this.nameDialog = true;
                    this.StartWhiteboard();
                    window.addEventListener('resize', this.HandelResize);
                }
                else{
                    this.$root.$confirm(this.$locales.t('noLiveModal.title'), 
                    this.$locales.t('noLiveModal.text'), {
                        cancelBtn: false,
                        okBtn: true
                    }).then(r => this.RedirectHome());
                }
            })
            .catch(e => {
                // error happened
                this.$root.$confirm(this.$locales.t('noLiveModal.title'), 
                this.$locales.t('noLiveModal.text'), {
                    cancelBtn: false,
                    okBtn: true
                }).then(r => this.RedirectHome())
            });
        },
        StartWhiteboard(){
            this.lineColor = this.$vuetify.theme.isDark ? '#fff' : '#000'
            var dimensions = this.GetWidthHeight();
            this.pad = new Sketchpad(this.$refs['sketchpad'], {
                aspectRatio: window.devicePixelRatio,
                width: dimensions.width,
                height: dimensions.hieght,
                line: {
                    color: this.lineColor
                }
            });
        },
        InitSignalR(){
            this.$qalamHub.$on('live-receive-message', this.StudentSendMessage)
            this.$qalamHub.$on('live-student-like', this.StudentSendLike)
            this.$qalamHub.$on('live-student-raise-hand', this.StudentRaiseHand)
            this.$qalamHub.$on('live-student-repeat', this.StudentNeedRepeat)
            this.$qalamHub.$on('user-join-group', this.NewUserJoin)
            this.$qalamHub.$on('user-leave-group', this.UserLeft)
            this.$qalamHub.$on('connected', this.SignalRConnected)
            this.$qalamHub.$emit('hub-connected');
        },
        SignalRConnected(){
            this.$qalamHub.$emit('join-group', this.liveId, this.user.name, true, 10);
        },
        MouseMove(event){
            this.$eventBus.$emit("MouseMove", event);
        },
        SetLineColor(color) {
            this.pad.setLineColor(color);
        },
        SetLineSize(size) {
            this.pad.setLineSize(size);
        },
        PenDraw(){
            this.SetLineColor(this.lineColor);
            this.SetLineSize(5);
        },
        UndoDraw() {
            this.pad.undo();
        },
        RedoDraw() {
            this.pad.redo();
        },
        ClearBoard() {
            this.pad.clear();
        },
        Erase(){
            let color = this.$vuetify.theme.isDark ? '#000' : '#fff'
            this.SetLineColor(color);
            this.SetLineSize(50);
        },
        ToggleFullScreen() {
            if (!this.IsFullScreen()) {
                this.OpenFullScreen(this.$refs['sketchpad']);
            }
            else {
                this.ExitFullscreen();
            }
        },
        ToggleChatIcon() {
            if(this.canSenMessage)
                this.buttons[2].icon = "fas fa-comment";
            else{
                this.buttons[2].icon = "fas fa-comment-slash";
            }
            this.canSenMessage = !this.canSenMessage;
            this.$qalamHub.$emit('live-toggle-messages', this.canSenMessage, this.liveId);
        },
        StudentSendMessage(message){
            this.$eventBus.$emit('newMessage', message);
        },
        StudentSendLike(studentName){
            
        },
        StudentRaiseHand(studentName){

        },
        StudentNeedRepeat(studentName){

        },
        NewUserJoin(userName, count){
            count = parseInt(count)
            if(isNaN(count))
                count = 0;
            this.numberOfStudent = count;
            this.Notify({
                audio: true,
                title: "طالب جديد",
                text: `${userName} Has Join the Group`,
            })
        },
        UserLeft(userName, count){
            count = parseInt(count)
            if(isNaN(count))
                count = 0;
            this.numberOfStudent = count;
            this.Notify({
                audio: true,
                title: "طالب غادر",
                text: `${userName} Has Left the Group`,
            })
        },
        GetWidthHeight() {
            return {
                hieght: window.innerHeight,
                width: this.$refs['sketchpad'].offsetWidth
            };
        },
        HandelResize(e) {
            var dim;
            if (this.IsFullScreen()) {
                dim = this.GetWidthHeight();
            }
            else {
                dim = this.GetWidthHeight();
            }
            this.pad.opts.aspectRatio = dim.hieght / dim.width;
            this.pad.resize(dim.width);
        },
        ChangeMode(darkMode){
            if(darkMode){
                this.lineColor = "#fff"
            }
            else{
                this.lineColor = "#000";
            }
            this.PenDraw();
            this.pad.redraw();
        },
        ModalDone(liveStream){
            Object.assign(this.liveStream, liveStream);
            this.nameDialog = false;
            this.InitSignalR();
        },
        ModalClosed(){
            this.nameDialog = false
            this.$root.$confirm(this.$locales.t('noLiveModal.title'), 
                this.$locales.t('noLiveModal.text'), {
                    cancelBtn: false,
                    okBtn: true
            }).then(r => this.RedirectHome())
        },
        EndLive(){
            // Close Live
            this.$root.$confirm(this.$locales.t('endLiveModal.title'), 
                this.$locales.t('endLiveModal.text'), {
                    cancelBtn: true,
                    okBtn: true
            }).then(r => { 
                if(r){   
                    this.$qalamHub.$emit('end-live', this.liveId);
                    this.RedirectHome()
                }
            });
        }
    }

}
</script>

<style>
.white-board {
    border: double 1px black;
    padding: 0;
    background-color: #fff;
    border-radius: 5px;
    border: 10px solid #adb2bd;
    box-shadow: inset -1px 2px 2px #404040, 6px 9px 1px rgba(0, 0, 0, 0.1);
    overflow: hidden;
    height: 90vh;
}
.white-board.dark{
    background-color: #272727;
}
.whiteboard-container{
    margin-top: 77px !important;
}
html, body {
    margin: 0 !important;
    height: 100%;
}
.v-content{
    padding: 0;
}
</style>