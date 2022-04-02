<template>
    <footer class="site-footer img-background">
        <div class="hide-background">
            <v-container>
                <v-row no-gutters justify="center">
                    <v-col cols="11" md="6" :class="$vuetify.rtl ? 'offset-md-1' : ''">
                        <h4 class="site-name headline primary--text">{{$locales.t('about').replace(/{site-name}/g, $locales.t('appName'))}}</h4>
                        <p class="text-justify" 
                        v-html="$locales.t('aboutSiteParagraph').replace(/{site-name}/g, `<span class='primary--text mb-3'>${$locales.t('appName')}</span>`)"/>
                    </v-col>

                    <v-col cols="5" md="2" :class="!$vuetify.rtl ? 'offset-md-1' : ''">
                        <h4 class="site-name headline primary--text">{{$locales.t('appName')}}</h4>
                        <p><router-link tag="a" to="/live">{{$locales.t('appName')}} {{$locales.t('live')}}</router-link></p>
                        <v-badge overlap color="primary" class="badge-to-btn" :class="{right : $vuetify.rtl}" :content="$locales.t('soonBadge')">
                             <p><v-btn @click="dialog = true" text disabled color="white">{{$locales.t('exams')}}</v-btn></p>
                        </v-badge>
                        <br />
                        <v-badge overlap color="primary" class="badge-to-btn" :class="{right : $vuetify.rtl}" :content="$locales.t('soonBadge')">
                             <p><v-btn to="/contests" text disabled color="white">{{$locales.t('contests')}}</v-btn></p>
                        </v-badge>
                        <p><router-link tag="a" to="/teachers">{{$locales.t('teachers')}}</router-link></p>
                        <p><router-link tag="a" to="/about-us">{{$locales.t('aboutus')}}</router-link></p>
                    </v-col>
                    <v-col cols="5" md="3">
                        <h4 class="site-name headline primary--text">{{$locales.t('callus')}}</h4>
                        <p><i class="fas fa-map-marker-alt"></i>{{$locales.t('location')}}</p>
                        <p><i class="fas fa-phone"></i> 201000000000+</p>
                        <p><i class="fas fa-envelope"></i> {{$locales.t('siteEmail')}}<a href="mail@domain.com">mail@domain.com</a></p>
                    </v-col>
                </v-row>
                <hr>
            </v-container>
            
            <v-container>
                <v-row no-gutters justify="center">
                    <v-col md="8" sm="5" cols="11">
                        <p class="footer-P headline">
                            {{$locales.t('rights')}}<span class="site-name primary--text">{{$locales.t('appName')}} </span>
                            {{fullYear}}  &copy;
                        </p>
                    </v-col>
                    <v-col md="4" sm="5" cols="11">
                        <ul class="social-icons">
                            <li><a class="facebook" href="https://www.facebook.com" target="blank"><i class="fab fa-facebook-f"></i></a></li>
                            <li><a class="twitter" href="https://www.twitter.com" target="blank"><i class="fab fa-twitter"></i></a></li>
                            <li><a class="dribbble" href="https://www.youtube.com" target="blank"><i class="fab fa-youtube"></i></a></li>
                            <li><a class="linkedin" href="https://www.google.com" target="blank"><i class="fab fa-google-plus-g"></i></a></li>
                        </ul>
                    </v-col>
                </v-row>
            </v-container>
        </div>

        <exam-selector :dialog="dialog" @close="ModalClosed" @done="ModalDone" />
    </footer>
</template>

<script>
import ExamSelector from '@/components/modals/exam-selector.vue'

export default {
    name: "DefaultFooter",

    components: {
        ExamSelector
    },

    data: () => ({
        fullYear: new Date().getFullYear(),
        dialog: false
    }),

    methods: {
        ModalClosed() {
            this.dialog = false
        },
        ModalDone() {
            this.dialog = false
        }
    }
}
</script>

<style scoped>
.btn-bg-transparent::before{
    background-color: transparent;
}
.hide-background{
    padding:45px 0 20px;
    background:rgba(9,9,9,0.8);
    color:#DDD;
}
.badge-to-btn:not(.right){
    right: 20px;
}
.right.badge-to-btn{
    left: 20px;
}

.site-footer{
    background-color:#26272b;
    font-size:15px;
    line-height:24px;
    color:#737373 !important;
}
.site-footer hr{
    border-top-color:#bbb;
    opacity:0.5
}
.site-footer hr.small{
    margin:20px 0
}
.site-footer h6{
    color:#fff;
    font-size:16px;
    text-transform:uppercase;
    margin-top:5px;
    letter-spacing:2px
}
.site-footer a, .theme--light.v-btn.v-btn--disabled{
    color:#737373 !important;
    text-decoration: none;
}
.site-footer a:hover{
    color:white;
    text-decoration:none;
}
.footer-links p{
    line-height: 15px;
}
.footer-links a{
    color:#fff;
}
.footer-links a:active,.footer-links a:focus,.footer-links a:hover{
    color:#fff;
    text-decoration:none;
}
.site-footer .social-icons{
    text-align:right
}
.site-footer .social-icons a{
    width:40px;
    height:40px;
    line-height:40px;
    margin-left:6px;
    margin-right:0;
    border-radius:100%;
    background-color:#33353d
}
.copyright-text{
    margin:0
}
@media (max-width:991px){
    .site-footer [class^=col-]{
        margin-bottom:30px
    }
}
@media (max-width:767px){
    .site-footer{
        padding-bottom:0
    }
    .site-footer .copyright-text,.site-footer .social-icons{
        text-align:center
    }
}
.social-icons{
    padding-left:0;
    margin-bottom:0;
    list-style:none
}
.social-icons li{
    display:inline-block;
    margin-bottom:4px
}
.social-icons li.title{
    margin-right:15px;
    text-transform:uppercase;
    color:#96a2b2;
    font-weight:700;
    font-size:13px
}
.social-icons a{
    background-color:#eceeef;
    color:#818a91;
    font-size:16px;
    display:inline-block;
    line-height:44px;
    width:44px;
    height:44px;
    text-align:center;
    margin-right:8px;
    border-radius:100%;
    -webkit-transition:all .2s linear;
    -o-transition:all .2s linear;
    transition:all .2s linear
}
.social-icons a:active,.social-icons a:focus,.social-icons a:hover{
    color:#fff;
    background-color:#29aafe
}
.social-icons.size-sm a{
    line-height:34px;
    height:34px;
    width:34px;
    font-size:14px
}
.social-icons a.facebook:hover{
    background-color:#3b5998
}
.social-icons a.twitter:hover{
    background-color:#00aced
}
.social-icons a.linkedin:hover{
    background-color:#dc4a40
}
.social-icons a.dribbble:hover{
    background-color:#FF0000
}
@media (max-width:767px)
{
  .social-icons li.title{
    display:block;
    margin-right:0;
    font-weight:600
  }
}

.img-background{
    background:url("~@/assets/images/BackFooter.jpg") no-repeat center;
    background-size:cover;
}
</style>