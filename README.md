# Reddit.NET
A Reddit API library for .NET Core with OAuth support.  Written in C#.

Currently Supported Endpoints:

GET /api/v1/me  
GET /api/v1/me/karma  
GET /api/v1/me/prefs  
PATCH /api/v1/me/prefs  
GET /api/v1/me/trophies  
GET /prefs/<where>  

POST /api/v1/<subreddit>/emoji_asset_upload_s3.json  
GET /api/v1/<subreddit>/emojis/all  

POST [/r/<subreddit>]/api/clearflairtemplates  
POST [/r/<subreddit>]/api/deleteflair  
POST [/r/<subreddit>]/api/deleteflairtemplate  
POST [/r/<subreddit>]/api/flair  
POST [/r/<subreddit>]/api/flairconfig  
POST [/r/<subreddit>]/api/flaircsv  
GET [/r/<subreddit>]/api/flairlist  
POST [/r/<subreddit>]/api/flairselector  
POST [/r/<subreddit>]/api/flairtemplate  
POST [/r/<subreddit>]/api/flairtemplate_v2  
GET [/r/<subreddit>]/api/link_flair  
GET [/r/<subreddit>]/api/link_flair_v2  
POST [/r/<subreddit>]/api/setflairenabled  
GET [/r/<subreddit>]/api/user_flair  
GET [/r/<subreddit>]/api/user_flair_v2  

POST /api/comment  
POST /api/del  
POST /api/editusertext  
POST /api/hide  
GET [/r/<subreddit>]/api/info  
POST /api/lock  
POST /api/marknsfw  
GET /api/morechildren  
POST /api/report  
POST /api/save  
POST /api/sendreplies  
POST /api/set_contest_mode  
POST /api/set_subreddit_sticky  
POST /api/set_suggested_sort  
POST /api/spoiler  
POST /api/submit  
POST /api/unhide  
POST /api/unlock  
POST /api/unmarknsfw  
POST /api/unsave  
POST /api/unspoiler  

GET /best  
GET /by_id/<names>  
GET [/r/<subreddit>]/comments/<article>  
GET /duplicates/<article>  
GET [/r/<subreddit>]/hot  
GET [/r/<subreddit>]/new  
GET [/r/<subreddit>]/random  
GET [/r/<subreddit>]/rising  
GET [/r/<subreddit>]/<sort>  

POST /api/live/create  
POST /api/live/<thread>/accept_contributor_invite  
POST /api/live/<thread>/close_thread  
POST /api/live/<thread>/delete_update  
POST /api/live/<thread>/edit  
POST /api/live/<thread>/invite_contributor  
POST /api/live/<thread>/leave_contributor  
POST /api/live/<thread>/report  
POST /api/live/<thread>/rm_contributor  
POST /api/live/<thread>/rm_contributor_invite  
POST /api/live/<thread>/set_contributor_permissions  
POST /api/live/<thread>/strike_update  
POST /api/live/<thread>/update  
GET /live/<thread>  
GET /live/<thread>/about  
GET /live/<thread>/contributors  
GET /live/<thread>/updates/<update_id>  

POST /api/block  
POST /api/collapse_message  
POST /api/compose  
POST /api/del_msg  
POST /api/read_all_messages  
POST /api/read_message  
POST /api/uncollapse_message  
POST /api/unread_message  
GET /message/<where>  

GET [/r/<subreddit>]/api/saved_media_text  
GET /api/v1/scopes  

GET [/r/<subreddit>]/about/log  
GET [/r/<subreddit>]/about/<location>  
POST [/r/<subreddit>]/api/accept_moderator_invite  
POST /api/approve  
POST /api/distinguish  
POST /api/ignore_reports  
POST /api/leavecontributor  
POST /api/leavemoderator  
POST /api/remove  
POST /api/unignore_reports  
GET [/r/<subreddit>]/stylesheet  

GET /api/mod/conversations  
POST /api/mod/conversations  
GET /api/mod/conversations/<conversation_id>  
POST /api/mod/conversations/<conversation_id>  
DELETE /api/mod/conversations/<conversation_id>/highlight  
POST /api/mod/conversations/<conversation_id>/highlight  
POST /api/mod/conversations/<conversation_id>/mute  
POST /api/mod/conversations/<conversation_id>/unmute  
GET /api/mod/conversations/<conversation_id>/user  
POST /api/mod/conversations/read  
GET /api/mod/conversations/subreddits  
POST /api/mod/conversations/unread  
GET /api/mod/conversations/unread/count  

POST /api/multi/copy  
GET /api/multi/mine  
POST /api/multi/rename  
GET /api/multi/user/<username>  
DELETE /api/multi/<multipath>  
GET /api/multi/<multipath>  
POST /api/multi/<multipath>  
PUT /api/multi/<multipath>  
GET /api/multi/<multipath>/description  
PUT /api/multi/<multipath>/description  
DELETE /api/multi/<multipath>/r/<srname>  
GET /api/multi/<multipath>/r/<srname>  
PUT /api/multi/<multipath>/r/<srname>  

GET [/r/<subreddit>]/about/<where>  
POST [/r/<subreddit>]/api/delete_sr_banner  
POST [/r/<subreddit>]/api/delete_sr_header  
POST [/r/<subreddit>]/api/delete_sr_icon  
POST [/r/<subreddit>]/api/delete_sr_img  
GET /api/search_reddit_names  
POST /api/search_subreddits  
POST /api/site_admin  
GET [/r/<subreddit>]/api/submit_text  
GET /api/subreddit_autocomplete  
GET /api/subreddit_autocomplete_v2  
POST [/r/<subreddit>]/api/subreddit_stylesheet  
POST /api/subscribe  
POST [/r/<subreddit>]/api/upload_sr_img  
GET /r/<subreddit>/about  
GET /r/<subreddit>/about/edit  
GET /r/<subreddit>/about/rules  
GET /r/<subreddit>/about/traffic  
GET /subreddits/mine/<where>  
GET /subreddits/search  
GET /subreddits/<where>  
GET /users/<where>  

POST /api/block_user  
POST [/r/<subreddit>]/api/friend  
POST /api/report_user  
POST [/r/<subreddit>]/api/setpermissions  
POST [/r/<subreddit>]/api/unfriend  
GET /api/user_data_by_account_ids  
GET /api/username_available  
DELETE /api/v1/me/friends/<username>  
GET /api/v1/me/friends/<username>  
PUT /api/v1/me/friends/<username>  
GET /api/v1/user/<username>/trophies  
GET /user/<username>/about  
GET /user/<username>/<where>  

POST /api/widget  
DELETE /api/widget/<widget_id>  
PUT /api/widget/<widget_id>  
PATCH /api/widget_order/<section>  
GET /api/widgets  

POST [/r/<subreddit>]/api/wiki/alloweditor/<act>  
POST [/r/<subreddit>]/api/wiki/edit  
POST [/r/<subreddit>]/api/wiki/hide  
POST [/r/<subreddit>]/api/wiki/revert  
GET [/r/<subreddit>]/wiki/pages  
GET [/r/<subreddit>]/wiki/revisions  
GET [/r/<subreddit>]/wiki/revisions/<page>  
GET [/r/<subreddit>]/wiki/settings/<page>  
POST [/r/<subreddit>]/wiki/settings/<page>  
GET [/r/<subreddit>]/wiki/<page>  

Total:  168 / 205 (82%)

There are 37 endpoints listed in the API docs that are not currently supported (mostly because I haven't been able to get them to work yet).

All 168 of the supported endpoints are covered by unit tests and all of those tests are passing.
