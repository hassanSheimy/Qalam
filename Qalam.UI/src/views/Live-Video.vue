<template>
    <div>
        <default-header />
        <circular-menu :buttons="buttons"/>
        <v-container fluid class="min-height">
            <v-row class="mt-4" no-gutters>
                <video class="video-js vjs-big-play-centered" ref="video-element" controls />
            </v-row>
        </v-container>
        <chat-head :title="$locales.t('questionComments')" position="right" :write="canWrite" @send="SendMessage" />
        <secondary-footer :fixed="false" />
        <rating-modal :dialog="reviewDialog" @done="ReviewModalDone" @close="ReviewModalClosed" />
    </div>
</template>

<script>
import DefaultHeader from '@/components/layout/default-header.vue'
import CircularMenu from '@/components/shared/circular-menu.vue'
import ChatHead from '@/components/shared/chat-head.vue'
import SecondaryFooter from '@/components/layout/secondary-footer.vue'
import videojs from 'video.js'
import RatingModal from '@/components/modals/rating-modal.vue'

export default {
    name: "LiveVideo",

    components: {
        DefaultHeader,
        CircularMenu,
        ChatHead,
        SecondaryFooter,
        RatingModal
    },

    data: () => ({
        stream: false,
        videoJsOptions: {
            autoplay: false,
            controls: true,
            sources: [{
                src: '',
                type: 'application/x-mpegURL'
            }],
            fluid: true,
        },
        player: null,
        hubConnection: null,
        liveId: null,
        canWrite: true,
        reviewDialog: false,
    }),

    computed: {
        buttons() {
            return [
                { callback: this.RequestRepeat, icon: 'fas fa-redo-alt' },
                { callback: this.Like, icon: 'fas fa-thumbs-up' },
                { callback: this.RaiseHand, icon: 'fas fa-hand-paper' }
            ]
        }
    },

    mounted() {
        this.liveId = this.$route.params.streamKey;
        if(this.IsThereUser()){
            this.StartStream();
            this.CheckStreamKey();
        }
        else{
            this.Unauthorized(this.$route.fullPath);
        }
    },

    methods: {
        async CheckStreamKey() {
            this.HttpRequest({
                url: 'api/live/watch/' + this.liveId,
                auth: true,
                method: 'POST'
            }).then(r => {
                if(r.data){
                    this.liveStreamId = r.data;
                    this.InitSignalR();
                }
                else{
                    this.$root.$confirm(this.$locales.t('accessDeniedModal.title'), 
                    this.$locales.t('accessDeniedModal.text'), {
                        cancelBtn: true,
                        okBtn: false
                    }).then(r => this.RedirectHome())
                }
            })
            .catch(e => {
                this.$root.$confirm(this.$locales.t('accessDeniedModal.title'), 
                this.$locales.t('accessDeniedModal.text'), {
                    cancelBtn: true,
                    okBtn: false
                }).then(r => this.RedirectHome())
            });
        },
        async StartStream() {
            this.stream = false;
            this.videoJsOptions.sources[0].src = `${this.$LIVE_APP_URL}/live/${this.liveId}/index.m3u8`;
            this.player = videojs(this.$refs['video-element'], this.videoJsOptions, function onPlayerReady() {
                this.on('error', function() {
                    this.reviewDialog = true
                });
                
                this.on('ended', function() {
                    this.reviewDialog = true
                });
            });
        },
        async InitSignalR(){
            this.$qalamHub.$on('live-receive-message', this.StudentSendMessage)
            this.$qalamHub.$on('live-teacher-toggle-message', this.TeacherToggleComments);
            this.$qalamHub.$on('no-host', this.NoHost);
            this.$qalamHub.$on('connected', this.SignalRConnected);
            this.$qalamHub.$emit('hub-connected');
        },
        SignalRConnected(){
            this.$qalamHub.$emit('join-group', this.liveId, this.user.name, false);
        },
        StudentSendMessage(message){
            this.$eventBus.$emit('newMessage', message);
        },
        TeacherToggleComments(isCommentsOpen){
            this.canWrite = isCommentsOpen;
        },
        SendMessage(message){
            this.$qalamHub.$emit('live-send-message', message, this.liveId)
        },
        RequestRepeat(){
            this.$qalamHub.$emit('live-repeat', this.user.name, this.liveId)
        },
        Like(){
            this.$qalamHub.$emit('live-like', this.user.name, this.liveId)
        },
        RaiseHand(){
            this.$qalamHub.$emit('live-raise-hand', this.user.name, this.liveId)
        },
        NoHost(){

        },
        ReviewModalDone(){
            this.reviewDialog = false
        },
        ReviewModalClosed(){
            this.reviewDialog = false
        }
    }
}
</script>

<style scoped>
.min-height{
    min-height: 70vh;
}
</style>