using System;

namespace NathansWay.iOS.Numeracy
{
	   
    public enum E__NumberComboEditMode
    {
        EditScroll = 1,
        EditUpDown = 2,
        EditNumPad = 3       
    }

	public enum E__FontNamesiOS
	{
//		AcademyEngravedLetPlain = 1,
//		AmericanTypewriter-CondensedLight = 2,
//		AmericanTypewriter-Light = 3,
//		AmericanTypewriter = 4,
//		AmericanTypewriter-Condensed = 5,
//		AmericanTypewriter-Bold = 6,
//		AmericanTypewriter-CondensedBold = 7,
//		AppleColorEmoji = 8,
//		AppleSDGothicNeo-Medium = 9,
//		AppleSDGothicNeo-Bold = 10 
//		ArialMT = 11,
//		Arial-ItalicMT = 12,
//		Arial-BoldMT = 13,
//		Arial-BoldItalicMT = 14,
//		ArialHebrew = 15,
//		ArialHebrew-Bold = 16,
//		ArialRoundedMTBold = 17,
//		BanglaSangamMN-Bold = 18,
//		BanglaSangamMN = 19,
//		Baskerville = 20,
//		Baskerville-Italic = 21,
//		Baskerville-SemiBold = 22,
//		Baskerville-SemiBoldItalic = 23,
//		Baskerville-Bold = 24,
//		Baskerville-BoldItalic= 25,
//		BodoniSvtyTwoITCTT-Book= 26,
//		BodoniSvtyTwoITCTT-BookIta= 27,
//		BodoniSvtyTwoITCTT-Bold= 28,
//		BodoniSvtyTwoOSITCTT-Book= 29,
//		BodoniSvtyTwoOSITCTT-BookIt= 30,
//		BodoniSvtyTwoOSITCTT-Bold= 31,
//		BodoniSvtyTwoSCITCTT-Book= 32,
//		BodoniOrnamentsITCTT= 33,
//		BradleyHandITCTT-Bold= 34,
//		ChalkboardSE-Light= 35,
//		ChalkboardSE-Regular= 36,
//		ChalkboardSE-Bold= 37,
//		Chalkduster= 38,
//		Cochin= 39,
//		Cochin-Italic= 24,
//		Cochin-Bold= 24,
//		Cochin-BoldItalic= 24,
//		Copperplate-Light= 24,
//		Copperplate= 24,
//		Copperplate-Bold= 24,
//		Courier= 24,
//		Courier-Oblique= 24,
//		Courier-Bold= 24,
//		Courier-BoldOblique= 24,
//		CourierNewPSMT= 24,
//		CourierNewPS-BoldMT= 24,
//		CourierNewPS-BoldItalicMT= 24,
//		CourierNewPS-ItalicMT= 24,
//		DBLCDTempBlack= 24,
//		DevanagariSangamMN= 24,
//		DevanagariSangamMN-Bold= 24,
//		Didot= 24,
//		Didot-Italic
//		Didot-Bold
//		EuphemiaUCAS
//		EuphemiaUCAS-Italic
//		EuphemiaUCAS-Bold
//		Futura-Medium
//		Futura-MediumItalic
//		Futura-CondensedMedium
//		Futura-CondensedExtraBold
//		GeezaPro
//		GeezaPro-Bold
//		Georgia
//		Georgia-Italic
//		Georgia-Bold
//		Georgia-BoldItalic
//		GillSans-Light
//		GillSans-LightItalic
//		GillSans
//		GillSans-Italic
//		GillSans-Bold
//		GillSans-BoldItalic
//		GujaratiSangamMN
//		GujaratiSangamMN-Bold
//		GurmukhiMN
//		GurmukhiMN-Bold
//		STHeitiSC-Light
//		STHeitiSC-Medium
//		STHeitiTC-Light
//		STHeitiTC-Medium
//		Helvetica-Light
//		Helvetica-LightOblique
//		Helvetica
//		Helvetica-Oblique
//		Helvetica-Bold
//		Helvetica-BoldOblique
//		HelveticaNeue-UltraLight
//		HelveticaNeue-UltraLightItalic
//		HelveticaNeue-Light
//		HelveticaNeue-LightItalic
//		HelveticaNeue
//		HelveticaNeue-Italic
//		HelveticaNeue-Medium
//		HelveticaNeue-Bold
//		HelveticaNeue-BoldItalic
//		HelveticaNeue-CondensedBold
//		HelveticaNeue-CondensedBlack
//		HiraKakuProN-W3
//		HiraKakuProN-W6
//		HiraMinProN-W3
//		HiraMinProN-W6
//		HoeflerText-Regular
//		HoeflerText-Italic
//		HoeflerText-Black
//		HoeflerText-BlackItalic
//		Kailasa
//		Kailasa-Bold
//		KannadaSangamMN
//		KannadaSangamMN-Bold
//		MalayalamSangamMN
//		MalayalamSangamMN-Bold
//		Marion-Regular
//		Marion-Italic
//		Marion-Bold
//		MarkerFelt-Thin
//		MarkerFelt-Wide
//		Noteworthy-Light
//		Noteworthy-Bold
//		Optima-Italic
//		Optima-Regular
//		Optima-Bold
//		Optima-BoldItalic
//		Optima-ExtraBlack
//		OriyaSangamMN
//		OriyaSangamMN-Bold
//		Palatino-Roman
//		Palatino-Italic
//		Palatino-Bold
//		Palatino-BoldItalic
//		Papyrus
//		Papyrus-Condensed
//		PartyLetPlain
//		SinhalaSangamMN
//		SinhalaSangamMN-Bold
//		SnellRoundhand
//		SnellRoundhand-Bold
//		SnellRoundhand-Black
//		TamilSangamMN
//		TamilSangamMN-Bold
//		TeluguSangamMN
//		TeluguSangamMN-Bold
//		Thonburi
//		Thonburi-Bold
//		TimesNewRomanPSMT
//		TimesNewRomanPS-ItalicMT
//		TimesNewRomanPS-BoldMT
//		TimesNewRomanPS-BoldItalicMT
//		TrebuchetMS
//		TrebuchetMS-Italic
//		TrebuchetMS-Bold
//		Trebuchet-BoldItalic
//		Verdana
//		Verdana-Italic
//		Verdana-Bold
//		Verdana-BoldItalic
//		ZapfDingbatsITC
//		Zapfino
	}
	   
}


