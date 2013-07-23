using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;

namespace OTS.WebLib.utilities
{
    /*****************************************************************************
     * 
                application/andrew-inset 	ez
                application/mac-binhex40 	hqx
                application/mac-compactpro 	cpt
                application/mathml+xml 	mathml
                application/msword 	doc
                application/octet-stream 	bin dms lha lzh exe class so dll
                application/oda 	oda
                application/ogg 	ogg
                application/pdf 	pdf
                application/postscript 	ai eps ps
                application/rdf+xml 	rdf
                application/smil 	smi smil
                application/srgs 	gram
                application/srgs+xml 	grxml
                application/vnd.mif 	mif
                application/vnd.mozilla.xul+xml 	xul
                application/vnd.ms-excel 	xls
                application/vnd.ms-powerpoint 	ppt
                application/vnd.wap.wbxml 	wbxml
                application/vnd.wap.wmlc 	.wmlc wmlc
                application/vnd.wap.wmlscriptc 	.wmlsc wmlsc
                application/voicexml+xml 	vxml
                application/x-bcpio 	bcpio
                application/x-cdlink 	vcd
                application/x-chess-pgn 	pgn
                application/x-cpio 	cpio
                application/x-csh 	csh
                application/x-director 	dcr dir dxr
                application/x-dvi 	dvi
                application/x-futuresplash 	spl
                application/x-gtar 	gtar
                application/x-hdf 	hdf
                application/x-httpd-php 	.php .php4 .php3 .phtml
                application/x-httpd-php-source 	.phps
                application/x-javascript 	js
                application/x-koan 	skp skd skt skm
                application/x-latex 	latex
                application/x-netcdf 	nc cdf
                application/x-pkcs7-crl 	.crl
                application/x-sh 	sh
                application/x-shar 	shar
                application/x-shockwave-flash 	swf
                application/x-stuffit 	sit
                application/x-sv4cpio 	sv4cpio
                application/x-sv4crc 	sv4crc
                application/x-tar 	.tgz tar
                application/x-tcl 	tcl
                application/x-tex 	tex
                application/x-texinfo 	texinfo texi
                application/x-troff 	t tr roff
                application/x-troff-man 	man
                application/x-troff-me 	me
                application/x-troff-ms 	ms
                application/x-ustar 	ustar
                application/x-wais-source 	src
                application/x-x509-ca-cert 	.crt
                application/xhtml+xml 	xhtml xht
                application/xml 	xml xsl
                application/xml-dtd 	dtd
                application/xslt+xml 	xslt
                application/zip 	zip
                audio/basic 	au snd
                audio/midi 	mid midi kar
                audio/mpeg 	mpga mp2 mp3
                audio/x-aiff 	aif aiff aifc
                audio/x-mpegurl 	m3u
                audio/x-pn-realaudio 	ram rm
                audio/x-pn-realaudio-plugin 	rpm
                audio/x-realaudio 	ra
                audio/x-wav 	wav
                chemical/x-pdb 	pdb
                chemical/x-xyz 	xyz
                image/bmp 	bmp
                image/cgm 	cgm
                image/gif 	gif
                image/ief 	ief
                image/jpeg 	jpeg jpg jpe
                image/png 	png
                image/svg+xml 	svg
                image/tiff 	tiff tif
                image/vnd.djvu 	djvu djv
                image/vnd.wap.wbmp 	.wbmp wbmp
                image/x-cmu-raster 	ras
                image/x-icon 	ico
                image/x-portable-anymap 	pnm
                image/x-portable-bitmap 	pbm
                image/x-portable-graymap 	pgm
                image/x-portable-pixmap 	ppm
                image/x-rgb 	rgb
                image/x-xbitmap 	xbm
                image/x-xpixmap 	xpm
                image/x-xwindowdump 	xwd
                model/iges 	igs iges
                model/mesh 	msh mesh silo
                model/vrml 	wrl vrml
                text/calendar 	ics ifb
                text/css 	css
                text/html 	.shtml html htm
                text/plain 	asc txt
                text/richtext 	rtx
                text/rtf 	rtf
                text/sgml 	sgml sgm
                text/tab-separated-values 	tsv
                text/vnd.wap.wml 	.wml wml
                text/vnd.wap.wmlscript 	.wmls wmls
                text/x-setext 	etx
                video/mpeg 	mpeg mpg mpe
                video/quicktime 	qt mov
                video/vnd.mpegurl 	mxu
                video/x-msvideo 	avi
                video/x-sgi-movie 	movie
                x-conference/x-cooltalk 	ice
    
     **************************************************************************************************/

    
    /// <summary>
    /// Response Utilities
    /// Could be used for the following purpose
    /// 
    ///     + Download file
    ///     + Secure Image generator
    /// 
    /// Miguel.Vu.Pham
    /// </summary>
    public class ResponseUtilities
    {
        public const string RESPONSE_IMAGE_GIF_TYPE = "image/gif";
        public const string RESPONSE_IMAGE_JPEG_TYPE = "image/jpeg";
        public const string RESPONSE_IMAGE_TIFF_TYPE = "image/tiff";
        
        public const string RESPONSE_FILE_WORD_TYPE = "application/msword";
        public const string RESPONSE_FILE_RTF_TYPE = "application/rtf";
        public const string RESPONSE_FILE_EXCEL_TYPE = "application/x-excel";
        public const string RESPONSE_FILE_POWERPOINT_TYPE = "application/ms-powerpoint";
        public const string RESPONSE_FILE_PDF_TYPE = "application/pdf";
        public const string RESPONSE_FILE_ZIP_TYPE = "application/zip";

        /// <summary>
        /// get response object
        /// + Use for download file function
        /// + User for img generator
        /// </summary>
        public void responseFileObjectToClient(string responseType, string filePath)
        {
            if (responseType == null || responseType == "") return;
            if (filePath == null || filePath == "") return;

            HttpResponse response = HttpContext.Current.Response;
            FileInfo fInfo = null;
            FileStream fStream = null;
            BinaryReader br = null;

            try
            {
                fInfo = new FileInfo(filePath);
                long numBytes = fInfo.Length;

                fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fStream);
                
                byte[] data = br.ReadBytes((int)numBytes);

                response.Clear();
                response.ContentType = responseType;
                response.BinaryWrite(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                br.Close();
                fStream.Close();
            }            
        }

    }
}
